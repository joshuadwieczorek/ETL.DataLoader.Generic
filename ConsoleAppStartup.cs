using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AAG.Global.ExtensionMethods;

namespace ETL.DataLoader.Generic
{
    public class ConsoleAppStartup
    {
        public IConfiguration Configuration { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public ConsoleAppStartup(IConfiguration configuration)
            => Configuration = configuration;


        /// <summary>
        /// Install services.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
           => services.InstallServicesFromAssembly<ConsoleAppStartup>(Configuration);

        /// <summary>
        /// Configuration.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(
              IApplicationBuilder app
            , IWebHostEnvironment env)
        { }
    }
}