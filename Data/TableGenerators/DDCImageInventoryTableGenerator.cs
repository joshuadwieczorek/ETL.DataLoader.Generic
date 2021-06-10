using AAG.Global.Data;
using ETL.DataLoader.Generic.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Data.TableGenerators
{
    public class DDCImageInventoryTableGenerator : TableGeneratorBase<DDCImageModel>
    {

        public DDCImageInventoryTableGenerator(string tableName) : base(tableName){ }

        public override void Populate(IEnumerable<DDCImageModel> rows)
        {
            if (rows is null || !rows.Any())
                return;

            foreach (var row in rows)
                Populate(row);
        }

        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("ImageUrl", typeof(String)));
            Table.Columns.Add(new DataColumn("ImageIsStock", typeof(bool)));
            Table.Columns.Add(new DataColumn("ImageIndexNo", typeof(int)));
        }

        protected override void Populate(DDCImageModel row)
        {
            DataRow tableRow = Table.NewRow();
            tableRow["ImageUrl"] = row.ImageUrl;
            tableRow["ImageIsStock"] = row.ImageIsStock;
            tableRow["ImageIndexNo"] = row.ImageIndexNo;
            Table.Rows.Add(tableRow);
        }
    }
}
