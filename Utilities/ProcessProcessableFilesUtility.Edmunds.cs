using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Data;
using ETL.DataLoader.Generic.Data.TableGenerators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private async Task ProcessEdmundsFile(
              DbContext dbContext
            , CsvReader csvReader
            , ProcessableFiles processableFiles)
        {
            var accountsConnectionString = _configuration.GetConnectionString("Accounts");
            var accountsDbContext = new DbContext(_configuration, _logger, _bugSnag, accountsConnectionString);
            EdmundsTableGenerator tableGenerator = new EdmundsTableGenerator(processableFiles.DatabaseTableName, accountsDbContext);
            var records = csvReader.GetRecords<EdmundsFileModel>();
            tableGenerator.Populate(records);
            _logger.LogInformation($"Bulk copying table!");
            await dbContext.BulkCopy(tableGenerator.Table);
        }
    }
}