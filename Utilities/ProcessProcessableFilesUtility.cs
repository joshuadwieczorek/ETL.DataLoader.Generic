using ETL.DataLoader.Generic.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using ETL.DataLoader.Generic.Data;
using CsvHelper;
using System.Globalization;
using AAG.Global.ExtensionMethods;
using ETL.DataLoader.Generic.Contracts.FileModels;
using System.Text.RegularExpressions;

namespace ETL.DataLoader.Generic.Utilities
{
    public partial class ProcessProcessableFilesUtility : IProcessProcessableFilesUtility
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProcessProcessableFilesUtility> _logger;
        private readonly Bugsnag.IClient _bugSnag;
        private DateTime _reportDate;
        private string _frequency;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="bugSnag"></param>
        public ProcessProcessableFilesUtility(
              IConfiguration configuration
            , ILogger<ProcessProcessableFilesUtility> logger
            , Bugsnag.IClient bugSnag)
        {
            _configuration = configuration;
            _logger = logger;
            _bugSnag = bugSnag;
        }
      

        #region "Process files"
        /// <summary>
        /// Process processable files.
        /// </summary>
        /// <param name="processableFiles"></param>
        public async Task Process(ProcessableFiles processableFiles)
        {
            try
            {
                ValidateProcessableFiles(processableFiles);               

                var files = Directory.GetFiles(processableFiles.PathPickup, processableFiles.DirectorySearchPattern);
                var connectionString = _configuration.GetConnectionString(processableFiles.DatabaseConnectionName);
                var dbContext = new DbContext(_configuration, _logger, _bugSnag, connectionString);
                Func<DbContext, CsvReader, ProcessableFiles, Task> fileProcessor = null;

                foreach (var file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        if (!fileInfo.Exists)
                            throw new FileNotFoundException($"File '{file}' is not found!");

                        if (fileInfo.Length < 1)
                        {
                            _logger.LogWarning("File '{file}' cannot be processed; file length is lest that 1");
                            continue;
                        }

                        _logger.LogInformation($"Starting to process file '{file}'!");

                        var fileName = "";

                        using (var fileReader = new StreamReader(file))
                        {
                            using var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                            fileProcessor = DetermineFileProcessorActor(processableFiles.Vendor.Lower());

                            fileName = Path.GetFileNameWithoutExtension(file);

                            if (file.Contains("Gubagoo"))
                            {
                                _reportDate = DateTime.Parse(fileName.Substring(13));
                            }
                            else if (file.Contains("CarNow"))
                            {
                                if (!file.Contains("Monthly"))
                                {
                                    _reportDate = DateTime.Parse(fileName.Substring(19));
                                    _frequency = "Daily";
                                }
                                else
                                {
                                    _reportDate = DateTime.Parse(fileName.Substring(27));
                                    _frequency = "Monthly";
                                }
                            }


                            if (fileProcessor != null)
                                await fileProcessor(dbContext, csvReader, processableFiles);
                            else
                                _logger.LogWarning($"File could not be processed due to invalid 'Type'; given value was '{processableFiles.Vendor}'");

                        }
                            
                            var archivePath = Path.Combine(processableFiles.PathArchive, $"{fileName}_processedAt_{DateTime.Now:yyyy-MM-dd_HH.mm.ss.fffffff}.csv");


                            _logger.LogInformation($"Moving file '{file}' too {archivePath}!");
                            File.Move(file, archivePath);

                            _logger.LogInformation($"Completed processing file '{file}'!");
                        
                    }

                    catch (Exception e)
                    {
                        _bugSnag.Notify(e);
                        _logger.LogError("{e}", e);
                    }
                }

                _logger.LogInformation($"Executing post load stored procedure!");
                if (files.Length > 0 && processableFiles.PostLoadStoredProcedure.HasValue())
                    await dbContext.Execute(processableFiles.PostLoadStoredProcedure);

            }
            catch (Exception e)
            {
                _bugSnag.Notify(e);
                _logger.LogError("{e}", e);
            }
        }


        /// <summary>
        /// Validate processable files object.
        /// </summary>
        /// <param name="processableFiles"></param>
        private void ValidateProcessableFiles(ProcessableFiles processableFiles)
        {
            if (processableFiles is null)
                throw new ArgumentNullException(nameof(processableFiles));

            if (!processableFiles.Vendor.HasValue())
                throw new ArgumentNullException("processableFiles.Type is null or empty!");

            if (!processableFiles.PathPickup.HasValue())
                throw new ArgumentNullException("processableFiles.PathPickup is null or empty!");

            if (!processableFiles.PathArchive.HasValue())
                throw new ArgumentNullException("processableFiles.PathArchive is null or empty!");

            if (!processableFiles.DirectorySearchPattern.HasValue())
                throw new ArgumentNullException("processableFiles.DirectorySearchPattern is null or empty!");

            if (!processableFiles.DatabaseConnectionName.HasValue())
                throw new ArgumentNullException("processableFiles.DatabaseConnectionName is null or empty!");

            if (!processableFiles.DatabaseTableName.HasValue())
                throw new ArgumentNullException("processableFiles.DatabaseTableName is null or empty!");

            if (!Directory.Exists(processableFiles.PathPickup))
                throw new DirectoryNotFoundException($"processableFiles.PathPickup: Directory '{processableFiles.PathPickup}' not found!");

            if (!Directory.Exists(processableFiles.PathArchive))
                throw new DirectoryNotFoundException($"processableFiles.PathArchive: Directory '{processableFiles.PathArchive}' not found!");

            if (!_configuration.GetConnectionString(processableFiles.DatabaseConnectionName).HasValue())
                throw new ArgumentNullException($"processableFiles.DatabaseConnectionName: ConnectionString of key/name '{processableFiles.DatabaseConnectionName}' cannot be found the appsettings.json!");
        }
        #endregion "Process files"
    }
}