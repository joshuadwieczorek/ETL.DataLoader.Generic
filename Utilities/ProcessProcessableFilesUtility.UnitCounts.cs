using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Data;
using ETL.DataLoader.Generic.Data.TableGenerators;
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
        private async Task ProcessUnitCountFile(
              DbContext dbContext
            , CsvReader csvReader
            , ProcessableFiles processableFiles)
        {
            UnitCountsTableGenerator tableGenerator = new UnitCountsTableGenerator(processableFiles.DatabaseTableName);
            var records = csvReader.GetRecords<UnitCountFileModel>();
            tableGenerator.Populate(records);
            _logger.LogInformation($"Bulk copying table!");
            await dbContext.BulkCopy(tableGenerator.Table);
        }
    }
}