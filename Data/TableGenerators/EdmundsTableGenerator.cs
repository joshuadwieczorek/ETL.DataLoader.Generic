using AAG.Global.Data;
using ETL.DataLoader.Generic.Contracts.FileModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Database.Accounts.Domain.accounts;
using AAG.Global.ExtensionMethods;

namespace ETL.DataLoader.Generic.Data.TableGenerators
{
    public class EdmundsTableGenerator : TableGeneratorBase<EdmundsFileModel>
    {
        private readonly DbContext _dbContext;
        private readonly List<Edmunds> _edmunds;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public EdmundsTableGenerator(
              string tableName
            , DbContext dbContext) : base(tableName) 
        {
            _dbContext = dbContext;
            _edmunds = new List<Edmunds>();
        }


        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("ID", typeof(long)));
            Table.Columns.Add(new DataColumn("EdmundsId", typeof(int)));
            Table.Columns.Add(new DataColumn("MonthYear", typeof(DateTime)));
            Table.Columns.Add(new DataColumn("PackageName", typeof(string)));
            Table.Columns.Add(new DataColumn("Budget", typeof(decimal)));
            Table.Columns.Add(new DataColumn("SRPViews", typeof(int)));
            Table.Columns.Add(new DataColumn("VDPViews", typeof(int)));
            Table.Columns.Add(new DataColumn("TotalEmails", typeof(int)));
            Table.Columns.Add(new DataColumn("TotalCalls", typeof(int)));
            Table.Columns.Add(new DataColumn("TotalCarcode", typeof(int)));
            Table.Columns.Add(new DataColumn("TotalLeads", typeof(int)));
            Table.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
            Table.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<EdmundsFileModel> rows)
        {
            if (rows is null || !rows.Any())
                return;

            ReadEdmundsAccounts();

            foreach (var row in rows)
                Populate(row);
        }


        /// <summary>
        /// Populate the row.
        /// </summary>
        /// <param name="row"></param>
        protected override void Populate(EdmundsFileModel row)
        {
            var account = GetEdmundsAccount(row.DealershipName);

            if (account is null)
            {
                CreateEdmundsAccount(row.DealershipName);
                account = GetEdmundsAccount(row.DealershipName);
            }            

            var tableRow = Table.NewRow();
            tableRow["ID"] = 0;
            tableRow["EdmundsId"] = account.EdmundsId;
            tableRow["MonthYear"] = row.DateMonth;
            tableRow["PackageName"] = row.PackageName;
            tableRow["Budget"] = row.PackageBudget;
            tableRow["SRPViews"] = row.SrpViews;
            tableRow["VDPViews"] = row.VdpViews;
            tableRow["TotalEmails"] = row.TotalEmails;
            tableRow["TotalCalls"] = row.TotalCalls;
            tableRow["TotalCarcode"] = row.TotalCarcode;
            tableRow["TotalLeads"] = (row.TotalEmails + row.TotalCalls);
            tableRow["CreatedBy"] = Environment.UserName;
            tableRow["CreatedAt"] = DateTime.Now;
            Table.Rows.Add(tableRow);
        }


        /// <summary>
        /// Get edmunds account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        private Edmunds GetEdmundsAccount(string accountName)
            => _edmunds
            .Where(a => a.AccountName.Lower() == accountName.Lower())
            .FirstOrDefault();


        /// <summary>
        /// Read all edmunds accounts.
        /// </summary>
        private void ReadEdmundsAccounts()
        {
            var accounts = _dbContext.QueryMulti<Edmunds>("dbo.EdmundsRead").Result;
            _edmunds.AddRange(accounts);
        }


        /// <summary>
        /// Create new edmunds account.
        /// </summary>
        /// <param name="accountName"></param>
        private void CreateEdmundsAccount(string accountName)
        {
            var parameters = new
            {
                @AccountName = accountName
            };

            var account = _dbContext.QuerySingle<Edmunds>("dbo.EdmundsCreate", parameters).Result;
            _edmunds.Add(account);
        }
    }
}