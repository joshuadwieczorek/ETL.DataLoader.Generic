using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETL.DataLoader.Generic.Contracts.FileModels;
using AAG.Global.Data;

namespace ETL.DataLoader.Generic.Data.TableGenerators
{
    public class UnitCountsTableGenerator : TableGeneratorBase<UnitCountFileModel>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public UnitCountsTableGenerator(string tableName) : base(tableName) { }


        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("AmsiAcronym", typeof(string)));
            Table.Columns.Add(new DataColumn("CountNew", typeof(int)));
            Table.Columns.Add(new DataColumn("CountUsed", typeof(int)));
            Table.Columns.Add(new DataColumn("CountUnwound", typeof(int)));
            Table.Columns.Add(new DataColumn("CountTotal", typeof(int)));
            Table.Columns.Add(new DataColumn("ReportDate", typeof(DateTime)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<UnitCountFileModel> rows)
        {
            if (rows is null || !rows.Any())
                return;

            foreach (var row in rows)
                Populate(row);
        }


        /// <summary>
        /// Populate the row.
        /// </summary>
        /// <param name="row"></param>
        protected override void Populate(UnitCountFileModel row)
        {
            var tableRow = Table.NewRow();
            tableRow["AmsiAcronym"] = row.Store;
            tableRow["CountNew"] = row.CountNew;
            tableRow["CountUsed"] = row.CountUsed;
            tableRow["CountUnwound"] = row.CountUnwound;
            tableRow["CountTotal"] = row.Total;
            tableRow["ReportDate"] = row.ReportDate;           
            Table.Rows.Add(tableRow);
        }
    }
}