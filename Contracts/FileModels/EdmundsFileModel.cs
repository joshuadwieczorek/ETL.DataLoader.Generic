using System;
using CsvHelper.Configuration.Attributes;

namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class EdmundsFileModel
    {
        [Name("f_cdd_dealership_id")]
        public long DealershipId { get; set; }

        [Name("package_name")]
        public string PackageName { get; set; }

        [Name("package_budget")]
        public string PackageBudget { get; set; }

        [Name("date_month")]
        public DateTime? DateMonth { get; set; }

        [Name("inventory_type")]
        public string InventoryType { get; set; }

        [Name("srp_views")]
        public string SrpViews { get; set; }

        [Name("vdp_views")]
        public string VdpViews { get; set; }

        [Name("eas_clickouts")]
        public string EasClickouts { get; set; }

        [Name("total_emails")]
        public string TotalEmails { get; set; }

        [Name("total_calls")]
        public string TotalCalls { get; set; }

        [Name("total_carcode")]
        public string TotalCarcode { get; set; }

        [Name("total_appraisals")]
        public int TotalAppraisals { get; set; }

        [Name("onsite_clickouts")]
        public int OnsiteClickouts { get; set; }

        [Name("dealership_name")]
        public string DealershipName { get; set; }
    }
}