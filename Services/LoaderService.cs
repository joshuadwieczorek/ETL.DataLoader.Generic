using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Utilities;
using AAG.Global.ExtensionMethods;
using Newtonsoft.Json;

namespace ETL.DataLoader.Generic.Services
{
    public class LoaderService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LoaderService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private Bugsnag.IClient bugSnag;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bugSnag"></param>
        /// <param name="leadProviderRepository"></param>
        public LoaderService(
              IHostApplicationLifetime applicationLifetime
            , ILogger<LoaderService> logger
            , IServiceProvider serviceProvider
            , IConfiguration configuration)
        {
            _applicationLifetime = applicationLifetime;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }


        /// <summary>
        /// On application startup.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Application starting!");
                var scope = _serviceProvider.CreateScope();
                var bugSnag = scope.ServiceProvider.GetRequiredService<Bugsnag.IClient>();

                var fileConfigurationPath = _configuration["FileConfigurationPath"];
                var timeoutMinutes = _configuration["ProcessLoopTimeoutInMinutes"].ToInt(5);

                if (!File.Exists(fileConfigurationPath))
                    throw new FileNotFoundException(fileConfigurationPath);
                
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var processableFiles = JsonConvert.DeserializeObject<List<ProcessableFiles>>(File.ReadAllText(fileConfigurationPath));

                        foreach (var processableFile in processableFiles)
                        {
                            var processableFileUtility = scope.ServiceProvider.GetRequiredService<IProcessProcessableFilesUtility>();
                            await processableFileUtility.Process(processableFile);
                        }
                    }
                    catch (Exception e)
                    {
                        bugSnag.Notify(e);
                        _logger.LogError("{e}", e);
                    }

                    // Wait for the timeout to complete before re-looping.
                    await Task.Delay((1000 * 60 * timeoutMinutes));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("{e}", e);
                bugSnag.Notify(e);
            }
        }


        /// <summary>
        /// On application shutdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application stopping!");
            await Task.CompletedTask;
        }
    }
}