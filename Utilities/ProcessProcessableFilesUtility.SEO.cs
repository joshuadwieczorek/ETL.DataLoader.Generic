using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Data;
using ETL.DataLoader.Generic.Data.TableGenerators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        private async Task ProcessSEOFile(
             DbContext dbContext
           , CsvReader csvReader
           , ProcessableFiles processableFiles)
        {
            SEOTableGenerator tableGenerator = new SEOTableGenerator(processableFiles.DatabaseTableName);
            csvReader.Configuration.Delimiter = "\t";
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.MissingFieldFound = null;
            var records = csvReader.GetRecords<SEOFileModel>().ToList();
            tableGenerator.Populate(records);
            _logger.LogInformation($"Bulk copying table!");
            await dbContext.BulkCopy(tableGenerator.Table);
        }
    }
}
