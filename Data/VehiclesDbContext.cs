using AAG.Global.Data;
using Dapper;
using ETL.DataLoader.Generic.Contracts.FileModels;
using ETL.DataLoader.Generic.Contracts.Models;
using ETL.DataLoader.Generic.Data.TableGenerators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETL.DataLoader.Generic.Data
{
    public class VehiclesDbContext : DbContext
    {
        private ILogger _logger;
        private SqlConnection _connection;
        
        public VehiclesDbContext(
              IConfiguration configuration
            ,  ILogger logger
            , Bugsnag.IClient bugSnag
            , string connectionString) : base(configuration, logger, bugSnag, connectionString) 
        {
            _logger = logger;
            _connection = new SqlConnection(connectionString);
        }

        public void PushToDatabase(DDCFileModel ddcFileModel)
        {   
            try
            {

                DDCImageInventoryTableGenerator imageInventoryTableGenerator = new DDCImageInventoryTableGenerator("[raw].[VehicleImagesTableType]");
                imageInventoryTableGenerator.Populate(ddcFileModel.Images);

                Object parameters = new
                {
                    @BodyStyle = ddcFileModel.BodyStyle,
                    @BodyType = ddcFileModel.BodyType,
                    @Certified = ddcFileModel.Certified,
                    @Comments = ddcFileModel.Comments,
                    @CreatedDate = ddcFileModel.CreatedDate,
                    @DealerId = ddcFileModel.DealerId,
                    @DealerName = ddcFileModel.DealerName,
                    @DetailPageUrl = ddcFileModel.DetailPageUrl,
                    @Doors = ddcFileModel.Doors,
                    @EngineCylinders = ddcFileModel.EngineCylinders,
                    @EngineDisplacement = ddcFileModel.EngineDisplacement,
                    @ExteriorColor = ddcFileModel.ExteriorColor,
                    @ExteriorColorCode = ddcFileModel.ExteriorColorCode,
                    @FuelTypeShort = ddcFileModel.FuelTypeShort,
                    @HasWarranty = ddcFileModel.HasWarranty,
                    @InteriorColor = ddcFileModel.InteriorColor,
                    @InternetPrice = ddcFileModel.InternetPrice,
                    @InternetPriceOrMSRP = ddcFileModel.InternetPriceOrMSRP,
                    @InventoryDate = ddcFileModel.InventoryDate,
                    @Invoice = ddcFileModel.Invoice,
                    @Iscpo = ddcFileModel.Iscpo,
                    @isdmsIndicatedSpecial = ddcFileModel.IsdmsIndicatedSpecial,
                    @IsphotosChanged = ddcFileModel.IsphotosChanged,
                    @IsplatFormSpecial = ddcFileModel.IsplatFormSpecial,
                    @Make = ddcFileModel.Make,
                    @Mileage = ddcFileModel.Mileage,
                    @Model = ddcFileModel.Model,
                    @ModelCode = ddcFileModel.ModelCode,
                    @MSRP = ddcFileModel.MSRP,
                    @PriceChangeDate = ddcFileModel.PriceChangeDate,
                    @StockNumber = ddcFileModel.StockNumber,
                    @Today = ddcFileModel.Today,
                    @UpdatedDate = ddcFileModel.UpdatedDate,
                    @URL = ddcFileModel.URL,
                    @Vin = ddcFileModel.Vin,
                    @Year = ddcFileModel.Year,
                    @Zip = ddcFileModel.Zip,
                    @Images = new TableValueParameter<DDCImageModel>(imageInventoryTableGenerator)
                };

                _connection.Execute("[raw].[DDCInventoryAndInventoryImagesLoader]", parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception e)
            {
                _bugSnag.Notify(e);
                _logger.LogError("{e}", e);
                throw;
            }
        }
    }
}