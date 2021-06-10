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
    public class CarNowTableGenerator : TableGeneratorBase<CarNowFileModel>
    {

        private readonly DbContext _dbContext;
        private readonly List<CarNow> _carnow;
        private DateTime _reportDate;
        private string _frequency;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName"></param>
        public CarNowTableGenerator(
              string tableName
            , DbContext dbContext
            , DateTime reportDate
            , string frequency) : base(tableName)
        {
            _dbContext = dbContext;
            _carnow = new List<CarNow>();
            _reportDate = reportDate;
            _frequency = frequency;
        }

        /// <summary>
        /// Construct table.
        /// </summary>
        protected override void Construct()
        {
            Table.Columns.Add(new DataColumn("ID", typeof(long)));
            Table.Columns.Add(new DataColumn("Dealer", typeof(string)));
            Table.Columns.Add(new DataColumn("CarNowId", typeof(int)));
            Table.Columns.Add(new DataColumn("Visits", typeof(int)));
            Table.Columns.Add(new DataColumn("Sales Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Service Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Part Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("BuyNow Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Other Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Total Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Chats / Visits", typeof(decimal)));
            Table.Columns.Add(new DataColumn("BDC Answered", typeof(int)));
            Table.Columns.Add(new DataColumn("DealNow Sent", typeof(int)));
            Table.Columns.Add(new DataColumn("DealNow Answered", typeof(int)));
            Table.Columns.Add(new DataColumn("DN Answered in 60", typeof(int)));
            Table.Columns.Add(new DataColumn("Answered / Deal Now", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Sales Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("Services Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("Parts Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("BuyNow Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("Other Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("Total Leads", typeof(int)));
            Table.Columns.Add(new DataColumn("Leads / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Appointments", typeof(int)));
            Table.Columns.Add(new DataColumn("Appts / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Response", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Engage Time (m)", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Desktop Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Desktop / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Mobile Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Mobile / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Tablet Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Tablet / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("SMS Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("SMS / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Facebook Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Facebook / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Other Device Chats", typeof(int)));
            Table.Columns.Add(new DataColumn("Other Device / Chats", typeof(decimal)));
            Table.Columns.Add(new DataColumn("Invites Sent", typeof(int)));
            Table.Columns.Add(new DataColumn("Invites Accepted", typeof(int)));
            Table.Columns.Add(new DataColumn("Accepted / Invites", typeof(decimal)));
            Table.Columns.Add(new DataColumn("ReportDate", typeof(DateTime)));
            Table.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
            Table.Columns.Add(new DataColumn("CreatedAt", typeof(DateTime)));
            Table.Columns.Add(new DataColumn("Frequency", typeof(string)));
        }


        /// <summary>
        /// Populate table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        public override void Populate(IEnumerable<CarNowFileModel> rows)
        {
            if (rows is null || !rows.Any())
                return;

            ReadCarNowAccounts();

            foreach (var row in rows)
                Populate(row);
        }


        /// <summary>
        /// Populate the row.
        /// </summary>
        /// <param name="row"></param>
        protected override void Populate(CarNowFileModel row)
        {

            var account = GetCarNowAccount(row.Dealer);

            if (account is null)
            {
                CreateCarNowAccount(row.Dealer);
                account = GetCarNowAccount(row.Dealer);
            }

            var tableRow = Table.NewRow();
            tableRow["ID"] = 0;
            tableRow["Dealer"] = row.Dealer;
            tableRow["CarNowId"] = account.CarNowId;
            tableRow["Visits"] = row.Visits.ToNValue();
            tableRow["Sales Chats"] = row.SalesChats.ToNValue();
            tableRow["Service Chats"] = row.ServiceChats.ToNValue();
            tableRow["Part Chats"] = row.PartChats.ToNValue();
            tableRow["BuyNow Chats"] = row.BuyNowChats.ToNValue();
            tableRow["Other Chats"] = row.OtherChats.ToNValue();
            tableRow["Total Chats"] = row.TotalChats.ToNValue();
            tableRow["Chats / Visits"] = row.ChatsDivVisits.ToNValue();
            tableRow["BDC Answered"] = row.BDCAnswered.ToNValue();
            tableRow["DealNow Sent"] = row.DealNowSent.ToNValue();
            tableRow["DealNow Answered"] = row.DealNowSent.ToNValue();
            tableRow["DN Answered in 60"] = row.DNAnsweredin60.ToNValue();
            tableRow["Answered / Deal Now"] = row.AnsweredDealNow.ToNValue();
            tableRow["Sales Leads"] = row.SalesLeads.ToNValue();
            tableRow["Services Leads"] = row.ServiceLeads.ToNValue();
            tableRow["Parts Leads"] = row.PartLeads.ToNValue();
            tableRow["BuyNow Leads"] = row.BuyNowLeads.ToNValue();
            tableRow["Other Leads"] = row.OtherLeads.ToNValue();
            tableRow["Total Leads"] = row.TotalLeads.ToNValue();
            tableRow["Leads / Chats"] = row.LeadsDivChats.ToNValue();
            tableRow["Appointments"] = row.Appointments.ToNValue();
            tableRow["Appts / Chats"] = row.ApptDivChats.ToNValue();
            tableRow["Response"] = row.ResponseTime.ToNValue();
            tableRow["Engage Time (m)"] = row.EngageTime.ToNValue();
            tableRow["Desktop Chats"] = row.DesktopChats.ToNValue();
            tableRow["Desktop / Chats"] = row.DesktopDivChats.ToNValue();
            tableRow["Mobile Chats"] = row.MobileChats.ToNValue();
            tableRow["Mobile / Chats"] = row.ChatMobileDivChats.ToNValue();
            tableRow["Tablet Chats"] = row.TabletChats.ToNValue();
            tableRow["Tablet / Chats"] = row.TabletDivChats.ToNValue();
            tableRow["SMS Chats"] = row.SMSChats.ToNValue();
            tableRow["SMS / Chats"] = row.SMSDivChats.ToNValue();
            tableRow["Facebook Chats"] = row.FacebookChats.ToNValue();
            tableRow["Facebook / Chats"] = row.FacebookDivChats.ToNValue();
            tableRow["Other Device Chats"] = row.OtherDeviceChats.ToNValue();
            tableRow["Other Device / Chats"] = row.OtherDeviceDivChats.ToNValue();
            tableRow["Invites Sent"] = row.InvitesSent.ToNValue();
            tableRow["Invites Accepted"] = row.InvitesAccepted.ToNValue(); 
            tableRow["Accepted / Invites"] = row.AcceptedDivInvites.ToNValue();
            tableRow["ReportDate"] = _reportDate;
            tableRow["CreatedBy"] = Environment.UserName.ToNValue();
            tableRow["CreatedAt"] = DateTime.Now.ToNValue();
            tableRow["Frequency"] = _frequency;
            Table.Rows.Add(tableRow);
        }

        /// <summary>
        /// Get edmunds account.
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        private CarNow GetCarNowAccount(string accountName)
            => _carnow
            .Where(a => a.AccountName.Lower() == accountName.Lower())
            .FirstOrDefault();


        ///// <summary>
        ///// Read all edmunds accounts.
        ///// </summary>
        private void ReadCarNowAccounts()
        {
            var accounts = _dbContext.QueryMulti<CarNow>("dbo.CarNowRead").Result;
            _carnow.AddRange(accounts);
        }


        ///// <summary>
        ///// Create new edmunds account.
        ///// </summary>
        ///// <param name="accountName"></param>
        private void CreateCarNowAccount(string accountName)
        {
            var parameters = new
            {
                @AccountName = accountName
            };

            var account = _dbContext.QuerySingle<CarNow>("dbo.CarNowCreate", parameters).Result;
            _carnow.Add(account);
        }

    }
}
