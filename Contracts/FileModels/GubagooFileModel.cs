using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class GubagooFileModel
    {
        [Ignore]
        public int Id { get; set; }

        [Name("Store")]
        public string Dealer { get; set; }

        [Name("Aggregated Unique Visitors (UV)")]
        public int AggregatedUniqueVisitors { get; set; }

        [Name("Total Chats Available")]
        public int TotalChatsAvailable { get; set; }

        [Name("Total Chats Available / UV")]
        public float TotalChatsAvailableDivUV { get; set; }

        [Name("Chats Handled")]
        public int ChatsHandled { get; set; }

        [Name("Resolved Chats (ResQ'd)")]
        public decimal ResolvedChats { get; set; }

        [Name("Missed Chats (ResQ'd)")]
        public decimal MissedChats { get; set; }

        [Name("SMS (Chats Handled)")]
        public int SMSChatsHandled { get; set; }

        [Name("Mobile (Chats Handled)")]
        public int MobileChatsHandled { get; set; }

        [Name("Desktop (Chats Handled)")]
        public int DesktopChatsHandled { get; set; }

        [Name("FB Messenger (Chats Handled)")]
        public int FBMessagerChatsHandled { get; set; }

        [Name("FB Marketplace (Chats Handled)")]
        public int FBMarketPlaceChatsHandled { get; set; }

        [Name("Apple Business Chat (Chats Handled)")]
        public int AppleBusinessChatsHandled { get; set; }

        [Name("Email (Chats Handled)")]
        public int EmailChatsHandled{ get; set; }

        [Name("Chats Handled (ResQ'd under 60 secs)")]
        public int ChatsHandledUnder60Secs { get; set; }

        [Name("Average Response Time (secs)")]
        public decimal AverageResponseTime { get; set; }

        [Name("Average Chat Duration (secs)")]
        public decimal AverageChatDuration { get; set; }

        [Name("Total Leads (Chats)")]
        public int TotalLeadsChats { get; set; }

        [Name("Concierge")]
        public int Concierge { get; set; }

        [Name("Chat Abandonment")]
        public int ChatAbandonment { get; set; }

        [Name("Appointments (Chats)")]
        public int AppointmentsChats { get; set; }

        [Name("Appointments")]
        public int Appointments { get; set; }

        [Name("Appointments / Chats Available")]
        public decimal AAppointmentsDivChatsAvailable { get; set; }

        [Ignore]
        public DateTime ReportDate { get; set; }

    }
}
