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
    public class GubagooTableGenerator : TableGeneratorBase<GubagooFileModel>
    {
        private readonly DbContext _dbContext;
        private readonly List<Gubagoo> _gubagoo;
        private  DateTime _reportDate;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public GubagooTableGenerator(
              string tableName
            , DbContext dbContext
            , DateTime reportDate) : base(tableName)
        {
            _dbContext = dbContext;
            _gubagoo = new List<Gubagoo>();
            _reportDate = reportDate;
        }

        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("ID", typeof(int)));
            Table.Columns.Add(new DataColumn("Dealer", typeof(string)));
            Table.Columns.Add(new DataColumn("GubagooId", typeof(int)));
            Table.Columns.Add(new DataColumn("Aggregated Unique Visitors (UV)", typeof(int)));
            Table.Columns.Add(new DataColumn("Total Chats Available", typeof(int)));
            Table.Columns.Add(new DataColumn("Total Chats Available / UV", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Chats Handled", typeof(int)));
            Table.Columns.Add(new DataColumn("Resolved Chats (ResQ'd)", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Missed Chats (ResQ'd)", typeof(decimal)));
            Table.Columns.Add(new DataColumn("SMS (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("Mobile (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("Desktop (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("FB Messenger (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("FB Marketplace (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("Apple Business Chat (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("Email (Chats Handled)", typeof(int)));
            Table.Columns.Add(new DataColumn("Chats Handled (ResQ'd under 60 secs)", typeof(int)));
            Table.Columns.Add(new DataColumn("Average Response Time (secs)", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Average Chat Duration (secs)", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Total Leads (Chats)", typeof(int)));
            Table.Columns.Add(new DataColumn("Concierge", typeof(int)));
            Table.Columns.Add(new DataColumn("Chat Abandonment", typeof(int)));
            Table.Columns.Add(new DataColumn("Appointments (Chats)", typeof(int)));
            Table.Columns.Add(new DataColumn("Appointments / Chats Available", typeof(decimal)));
            Table.Columns.Add(new DataColumn("ReportDate", typeof(DateTime)));
            Table.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
            Table.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<GubagooFileModel> rows)
        {
            if (rows is null || !rows.Any())
                return;

            ReadGubagooAccounts();

            foreach (var row in rows)
                Populate(row);
        }


        /// <summary>
        /// Populate the row.
        /// </summary>
        /// <param name="row"></param>
        protected override void Populate(GubagooFileModel row)
        {

            var account = GetGubagooAccount(row.Dealer);

            if (account is null)
            {
                CreateGubagooAccount(row.Dealer);
                account = GetGubagooAccount(row.Dealer);
            }

            var tableRow = Table.NewRow();
            tableRow["ID"] = 0;
            tableRow["Dealer"] = row.Dealer;
            tableRow["GubagooId"] = account.GubagooId;
            tableRow["Aggregated Unique Visitors (UV)"] = row.AggregatedUniqueVisitors.ToNValue();
            tableRow["Total Chats Available"] = row.TotalChatsAvailable.ToNValue();
            tableRow["Total Chats Available / UV"] = Math.Round(row.TotalChatsAvailableDivUV,6).ToNValue();
            tableRow["Chats Handled"] = row.ChatsHandled.ToNValue();
            tableRow["Resolved Chats (ResQ'd)"] = row.ResolvedChats.ToNValue();
            tableRow["Missed Chats (ResQ'd)"] = row.MissedChats.ToNValue();
            tableRow["SMS (Chats Handled)"] = row.SMSChatsHandled.ToNValue();
            tableRow["Mobile (Chats Handled)"] = row.MobileChatsHandled.ToNValue();
            tableRow["Desktop (Chats Handled)"] = row.DesktopChatsHandled.ToNValue();
            tableRow["FB Messenger (Chats Handled)"] = row.FBMessagerChatsHandled.ToNValue();
            tableRow["FB Marketplace (Chats Handled)"] = row.FBMarketPlaceChatsHandled.ToNValue();
            tableRow["Apple Business Chat (Chats Handled)"] = row.AppleBusinessChatsHandled.ToNValue();
            tableRow["Email (Chats Handled)"] = row.EmailChatsHandled.ToNValue();
            tableRow["Chats Handled (ResQ'd under 60 secs)"] = row.ChatsHandledUnder60Secs.ToNValue();
            tableRow["Average Response Time (secs)"] = row.AverageResponseTime.ToNValue();
            tableRow["Average Chat Duration (secs)"] = row.AverageChatDuration.ToNValue();
            tableRow["Total Leads (Chats)"] = row.TotalLeadsChats.ToNValue();
            tableRow["Concierge"] = row.Concierge.ToNValue();
            tableRow["Chat Abandonment"] = row.ChatAbandonment.ToNValue();
            tableRow["Appointments (Chats)"] = row.AppointmentsChats.ToNValue();
            tableRow["Appointments / Chats Available"] = row.AAppointmentsDivChatsAvailable.ToNValue();
            tableRow["ReportDate"] = _reportDate;
            tableRow["CreatedBy"] = Environment.UserName.ToNValue();
            tableRow["CreatedAt"] = DateTime.Now.ToNValue();
            Table.Rows.Add(tableRow);
        }

        /// <summary>
        /// Get edmunds account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        private Gubagoo GetGubagooAccount(string accountName)
            => _gubagoo
            .Where(a => a.AccountName.Lower() == accountName.Lower())
            .FirstOrDefault();


        ///// <summary>
        ///// Read all edmunds accounts.
        ///// </summary>
        private void ReadGubagooAccounts()
        {
            var accounts = _dbContext.QueryMulti<Gubagoo>("dbo.GubagooRead").Result;
            _gubagoo.AddRange(accounts);
        }


        ///// <summary>
        ///// Create new edmunds account.
        ///// </summary>
        ///// <param name="accountName"></param>
        private void CreateGubagooAccount(string accountName)
        {
            var parameters = new
            {
                @AccountName = accountName
            };

            var account = _dbContext.QuerySingle<Gubagoo>("dbo.GubagooCreate", parameters).Result;
            _gubagoo.Add(account);
        }
    }
}
