using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETL.DataLoader.Generic.Contracts.FileModels;
using AAG.Global.Data;

namespace ETL.DataLoader.Generic.Data.TableGenerators
{
    public class TrmTableGenerator : TableGeneratorBase<TrmFileModel>
    {
        private readonly DateTime _reportDate;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public TrmTableGenerator(string tableName) : base(tableName)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            _reportDate = new DateTime(lastMonth.Year, lastMonth.Month, 1);
        }


        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("EleadId", typeof(int)));
            Table.Columns.Add(new DataColumn("OpptysIn", typeof(int)));
            Table.Columns.Add(new DataColumn("BeBacks", typeof(int)));
            Table.Columns.Add(new DataColumn("Sold", typeof(int)));
            Table.Columns.Add(new DataColumn("WriteUps", typeof(int)));
            Table.Columns.Add(new DataColumn("Demo", typeof(int)));
            Table.Columns.Add(new DataColumn("ApptSet", typeof(int)));
            Table.Columns.Add(new DataColumn("ApptShow", typeof(int)));
            Table.Columns.Add(new DataColumn("ApptSold", typeof(int)));
            Table.Columns.Add(new DataColumn("FreshUps", typeof(int)));
            Table.Columns.Add(new DataColumn("FreshUpsSold", typeof(int)));
            Table.Columns.Add(new DataColumn("FreshApptShow", typeof(int)));
            Table.Columns.Add(new DataColumn("FreshApptSold", typeof(int)));
            Table.Columns.Add(new DataColumn("Be", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUps", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpsNew", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpsUsed", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpApptSet", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpApptShow", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpApptSold", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpsIn", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpsSold", typeof(int)));
            Table.Columns.Add(new DataColumn("PhoneUpsBE", typeof(int)));
            Table.Columns.Add(new DataColumn("InetUps", typeof(int)));
            Table.Columns.Add(new DataColumn("InetUpsNew", typeof(int)));
            Table.Columns.Add(new DataColumn("InetUpsUsed", typeof(int)));
            Table.Columns.Add(new DataColumn("InetApptSet", typeof(int)));
            Table.Columns.Add(new DataColumn("InetApptShow", typeof(int)));
            Table.Columns.Add(new DataColumn("InetApptSold", typeof(int)));
            Table.Columns.Add(new DataColumn("InetUpsIn", typeof(int)));
            Table.Columns.Add(new DataColumn("InetSold", typeof(int)));
            Table.Columns.Add(new DataColumn("InetBe", typeof(int)));
            Table.Columns.Add(new DataColumn("ReportDate", typeof(DateTime)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<TrmFileModel> rows)
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
        protected override void Populate(TrmFileModel row)
        {
            var tableRow = Table.NewRow();
            tableRow["EleadId"] = row.lChildID;
            tableRow["OpptysIn"] = row.OpptysIn;
            tableRow["BeBacks"] = row.BeBacks;
            tableRow["Sold"] = row.Sold;
            tableRow["WriteUps"] = row.WriteUps;
            tableRow["Demo"] = row.Demo;
            tableRow["ApptSet"] = row.lApptSet;
            tableRow["ApptShow"] = row.ApptShow;
            tableRow["ApptSold"] = row.ApptSold;
            tableRow["FreshUps"] = row.FreshUps;
            tableRow["FreshUpsSold"] = row.FreshUpsSold;
            tableRow["FreshApptShow"] = row.FreshApptShow;
            tableRow["FreshApptSold"] = row.FreshApptSold;
            tableRow["Be"] = row.BE;
            tableRow["PhoneUps"] = row.PhoneUps;
            tableRow["PhoneUpsNew"] = row.PhoneUpsNew;
            tableRow["PhoneUpsUsed"] = row.PhoneUpsUsed;
            tableRow["PhoneUpApptSet"] = row.PhoneUpApptSet;
            tableRow["PhoneUpApptShow"] = row.PhoneApptShow;
            tableRow["PhoneUpApptSold"] = row.PhoneApptSold;
            tableRow["PhoneUpsIn"] = row.PhoneUpsIn;
            tableRow["PhoneUpsSold"] = row.PhoneUpSold;
            tableRow["PhoneUpsBE"] = row.PhoneUpBE;
            tableRow["InetUps"] = row.InetUps;
            tableRow["InetUpsNew"] = row.InetUpsNew;
            tableRow["InetUpsUsed"] = row.InetUpsUsed;
            tableRow["InetApptSet"] = row.InetApptSet;
            tableRow["InetApptShow"] = row.InetApptShow;
            tableRow["InetApptSold"] = row.InetApptSold;
            tableRow["InetUpsIn"] = row.InetUpsIn;
            tableRow["InetSold"] = row.InetSold;
            tableRow["InetBe"] = row.InetBE;
            tableRow["ReportDate"] = _reportDate;
            Table.Rows.Add(tableRow);
        }
    }
}