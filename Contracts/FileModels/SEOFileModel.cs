using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace ETL.DataLoader.Generic.Contracts.FileModels
{
    public class SEOFileModel
    {
        [Ignore]
        public int Id { get; set; }

        [Name("Date")]
        public string ReportDate { get; set; }

        [Name("Domain")]
        public string Domain { get; set; }

        [Name("Location")]
        public string Location { get; set; }

        [Name("Keyword")]
        public string Keyword{ get; set; }

        [Name("Ranking URL")]
        public string RankingURL { get; set; }

        [Name("Rank")]
        public int Rank { get; set; }

        [Name("URL Type")]
        public string URLType { get; set; }

        [Name("Search Volume")]
        public int SearchVolume { get; set; }
    }
}
