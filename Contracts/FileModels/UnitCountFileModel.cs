using System;
using CsvHelper.Configuration.Attributes;

namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class UnitCountFileModel
    {
        [Name("Store")]
        public string Store { get; set; }

        [Name("New")]
        public int CountNew { get; set; }
        [Name("Used")]
        public int CountUsed { get; set; }

        [Name("Unwound")]
        public int CountUnwound { get; set; }

        [Name("Total")]
        public int Total { get; set; }

        [Name("ReportDate")]
        public DateTime ReportDate { get; set; }
    }
}