using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Data;
using ETL.DataLoader.Generic.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Utilities
{
    public partial class ProcessProcessableFilesUtility : IProcessProcessableFilesUtility
    {
        /// <summary>
        /// Process DDC File.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="csvReader"></param>
        /// <param name="processableFiles"></param>
        /// <returns></returns>
        private async Task ProcessDDCFile(
              DbContext dbContext
            , CsvReader csvReader
            , ProcessableFiles processableFiles)
        {
            var ddcHelper = new DDCHelper();
            var vehiclesDbContext = new VehiclesDbContext(_configuration, _logger, _bugSnag, _configuration.GetConnectionString("VehiclesV2"));
            var ddcFileInventories = csvReader.GetRecords<DDCFileModel>();

            foreach (var ddcFileInventory in ddcFileInventories)
            {
                try
                {
                    ddcFileInventory.IsSuccess = await ddcHelper.ValidateInventory(ddcFileInventory);

                    ddcFileInventory.Images = ddcHelper.BuildInventoryDDCImageModel(ddcFileInventory.ImageUrls, ddcFileInventory.ImageIsStock);

                    vehiclesDbContext.PushToDatabase(ddcFileInventory);
                }
                catch (Exception exception)
                {
                    _logger?.LogError("[InventoryLoaderService.EntryPoint.StartLoader] {exception}", exception);
                }
            }
        }
    }
}