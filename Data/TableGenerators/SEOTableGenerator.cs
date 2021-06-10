using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAG.Global.Data;
using ETL.DataLoader.Generic.Contracts.FileModels;
using AAG.Global.ExtensionMethods;
using System.Data;
using Database.Accounts.Domain.accounts;

namespace ETL.DataLoader.Generic.Data.TableGenerators
{
    public class SEOTableGenerator : TableGeneratorBase<SEOFileModel>
    {

        private readonly DbContext _dbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public SEOTableGenerator(
              string tableName
            ) : base(tableName)
        {
        }

        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("Id", typeof(int)));
            Table.Columns.Add(new DataColumn("ReportDate", typeof(string)));
            Table.Columns.Add(new DataColumn("Domain", typeof(string)));
            Table.Columns.Add(new DataColumn("Location", typeof(string)));
            Table.Columns.Add(new DataColumn("Keyword", typeof(string)));
            Table.Columns.Add(new DataColumn("RankingURL", typeof(string)));
            Table.Columns.Add(new DataColumn("Rank", typeof(int)));
            Table.Columns.Add(new DataColumn("URLType", typeof(string)));
            Table.Columns.Add(new DataColumn("SearchVolume", typeof(int)));
            Table.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            Table.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<SEOFileModel> rows)
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
        protected override void Populate(SEOFileModel row)
        {

            var tableRow = Table.NewRow();
            tableRow["Id"] = 0;
            tableRow["ReportDate"] = row.ReportDate;
            tableRow["Domain"] = row.Domain.ToNValue();
            tableRow["Location"] = row.Location.ToNValue();
            tableRow["Keyword"] = row.Keyword.ToNValue();
            tableRow["RankingURL"] = row.RankingURL.ToNValue();
            tableRow["Rank"] = row.Rank.ToNValue();
            tableRow["URLType"] = row.URLType.ToNValue();
            tableRow["SearchVolume"] = row.SearchVolume.ToNValue();
            tableRow["CreatedBy"] = Environment.UserName.ToNValue();
            tableRow["CreatedAt"] = DateTime.Now.ToNValue();
            Table.Rows.Add(tableRow);
        }


    }
}
