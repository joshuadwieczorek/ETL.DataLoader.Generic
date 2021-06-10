using CsvHelper.Configuration.Attributes;
using ETL.DataLoader.Generic.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class DDCFileModel
    {
        [Name("bodytype")]
        public String BodyType { get; set; }

        [Name("bodystyle")]
        public String BodyStyle { get; set; }

        [Name("certified")]
        public String Certified { get; set; }

        [Name("comments")]
        public String Comments { get; set; }

        [Name("createddate")]
        public String CreatedDate { get; set; }

        [Name("dealerid")]
        public String DealerId { get; set; }

        [Name("dealername")]
        public String DealerName { get; set; }

        [Name("detailpageurl")]
        public String DetailPageUrl { get; set; }

        [Name("doors")]
        public String Doors { get; set; }

        [Name("enginecylinderstostring")]
        public String EngineCylinders { get; set; }

        [Name("enginedisplacement")]
        public String EngineDisplacement { get; set; }

        [Name("exteriorcolor")]
        public String ExteriorColor { get; set; }

        [Name("exteriorcolorcode")]
        public String ExteriorColorCode { get; set; }

        [Name("firstimage")]
        public String FirstImage { get; set; }

        [Name("firstimagedate")]
        public String FirstImageDate { get; set; }

        [Name("fueltypeshort")]
        public String FuelTypeShort { get; set; }

        [Name("haswarranty")]
        public String HasWarranty { get; set; }

        [Name("images")]
        public String ImageUrls { get; set; }

        [Name("imagedate")]
        public String ImageDate { get; set; }

        [Name("imageisstock")]
        public String ImageIsStock { get; set; }

        [Name("interiorcolor")]
        public String InteriorColor { get; set; }

        [Name("internetprice")]
        public String InternetPrice { get; set; }

        [Name("internetpriceormsrp")]
        public String InternetPriceOrMSRP { get; set; }

        [Name("inventorydate")]
        public String InventoryDate { get; set; }

        [Name("invoice")]
        public String Invoice { get; set; }

        [Name("iscpo")]
        public String Iscpo { get; set; }

        [Name("isdmsindicatedspecial")]
        public String IsdmsIndicatedSpecial { get; set; }

        [Name("isphotoschanged")]
        public String IsphotosChanged { get; set; }

        [Name("isplatformspecial")]
        public String IsplatFormSpecial { get; set; }

        [Name("make")]
        public String Make { get; set; }

        [Name("mileage")]
        public String Mileage { get; set; }

        [Name("model")]
        public String Model { get; set; }

        [Name("modelcode")]
        public String ModelCode { get; set; }

        [Name("msrp")]
        public String MSRP { get; set; }

        [Name("pricechangedate")]
        public String PriceChangeDate { get; set; }

        [Name("stocknumber")]
        public String StockNumber { get; set; }

        [Name("today")]
        public String Today { get; set; }

        [Name("updateddate")]
        public String UpdatedDate { get; set; }

        [Name("url")]
        public String URL { get; set; }

        [Name("vin")]
        public String Vin { get; set; }

        [Name("year")]
        public String Year { get; set; }

        [Name("zip")]
        public String Zip { get; set; }

        [Ignore]
        public bool IsSuccess { get; set; }

        [Ignore]
        internal List<DDCImageModel> Images { get; set; }
    }
}