using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class CarNowFileModel
    {

        [Name("Id")]
        public int Id { get; set; }

        [Name("Dealer")]
        public string Dealer{ get; set; }

        [Name("Visits")]
        public int Visits { get; set; }

        [Name("Sales Chats")]
        public int SalesChats { get; set; }

        [Name("Service Chats")]
        public int ServiceChats { get; set; }

        [Name("Part Chats")]
        public int PartChats { get; set; }

        [Name("BuyNow Chats")]
        public string BuyNowChats { get; set; }

        [Name("Other Chats")]
        public int OtherChats { get; set; }

        [Name("Total Chats")]
        public decimal TotalChats { get; set; }

        [Name("Chats / Visits")]
        public decimal ChatsDivVisits { get; set; }

        [Name("BDC Answered")]
        public int BDCAnswered { get; set; }

        [Name("DealNow Sent")]
        public string DealNowSent{ get; set; }

        [Name("DealNow Answered")]
        public int DealNowAnswered { get; set; }

        [Name("DN Answered in 60")]
        public int DNAnsweredin60 { get; set; }

        [Name("Answered / DealNow")]
        public decimal AnsweredDealNow { get; set; }

        [Name("Sales Leads")]
        public int SalesLeads{ get; set; }

        [Name("Service Leads")]
        public string ServiceLeads{ get; set; }

        [Name("Part Leads")]
        public int PartLeads { get; set; }

        [Name("BuyNow Leads")]
        public int BuyNowLeads { get; set; }

        [Name("Other Leads")]
        public int OtherLeads { get; set; }

        [Name("Total Leads")]
        public int TotalLeads{ get; set; }

        [Name("Leads / Chats")]
        public decimal LeadsDivChats { get; set; }

        [Name("Appointments")]
        public int Appointments { get; set; }

        [Name("Appts / Chats")]
        public decimal ApptDivChats { get; set; }

        [Name("Response Time (s)")]
        public decimal ResponseTime    { get; set; }

        [Name("Engage Time (m)")]
        public string EngageTime { get; set; }

        [Name("Desktop Chats")]
        public int DesktopChats { get; set; }

        [Name("Desktop / Chats")]
        public decimal DesktopDivChats { get; set; }

        [Name("Mobile Chats")]
        public int MobileChats { get; set; }

        [Name("Mobile / Chats")]
        public decimal ChatMobileDivChats { get; set; }

        [Name("Tablet Chats")]
        public int TabletChats { get; set; }

        [Name("Tablet / Chats")]
        public decimal TabletDivChats{ get; set; }

        [Name("SMS Chats")]
        public int SMSChats { get; set; }

        [Name("SMS / Chats")]
        public decimal SMSDivChats { get; set; }

        [Name("Facebook Chats")]
        public int FacebookChats { get; set; }

        [Name("Facebook / Chats")]
        public decimal FacebookDivChats{ get; set; }

        [Name("Other Device Chats")]
        public int OtherDeviceChats { get; set; }

        [Name("Other Device / Chats")]
        public string OtherDeviceDivChats{ get; set; }

        [Name("Invites Sent")]
        public string InvitesSent{ get; set; }

        [Name("Invites Accepted")]
        public string InvitesAccepted{ get; set; }

        [Name("Accepted / Invites")]
        public string AcceptedDivInvites { get; set; }

        [Ignore]
        public DateTime ReportDate { get; set; }

        [Ignore]
        public string Frequency { get; set; }

    }
}

