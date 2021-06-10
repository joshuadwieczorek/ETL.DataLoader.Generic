using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Contracts.Models
{
    public class DDCImageModel
    {
        public int InventoryDDCImageImageId { get; set; }
        public int InventoryDDCId { get; set; }
        public bool ImageIsStock { get; set; }
        public String ImageUrl { get; set; }
        public int ImageIndexNo { get; set; }
    }
}
