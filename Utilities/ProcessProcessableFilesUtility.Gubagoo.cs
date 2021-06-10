using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Data;
using ETL.DataLoader.Generic.Data.TableGenerators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;


namespace ETL.DataLoader.Generic.Utilities
{
    public partial class ProcessProcessableFilesUtility : IProcessProcessableFilesUtility
    {
        /// <summary>
        /// Process edmunds file.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="csvReader"></param>
        /// <param name="processableFiles"></param>
        /// <returns></returns>
        private async Task ProcessGubagooFile(
             DbContext dbContext
           , CsvReader csvReader
           , ProcessableFiles processableFiles)
        {
            var reportingConnectionString = _configuration.GetConnectionString("Reporting");
            var reportingDbContext = new DbContext(_configuration, _logger, _bugSnag, reportingConnectionString);
            GubagooTableGenerator tableGenerator = new GubagooTableGenerator(processableFiles.DatabaseTableName, reportingDbContext, _reportDate);
            csvReader.Configuration.Delimiter = ",";
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.MissingFieldFound = null;
            var records = csvReader.GetRecords<GubagooFileModel>().ToList();
            tableGenerator.Populate(records);
            _logger.LogInformation($"Bulk copying table!");
            await dbContext.BulkCopy(tableGenerator.Table);
        }
    }
}
