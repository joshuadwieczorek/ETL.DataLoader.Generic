using AAG.Global.ExtensionMethods;
using CsvHelper;
using ETL.DataLoader.Generic.Contracts;
using ETL.DataLoader.Generic.Data;
using System;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Utilities
{
    public partial class ProcessProcessableFilesUtility : IProcessProcessableFilesUtility
    {
        /// <summary>
        /// Determine file processor actor.
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        private Func<DbContext, CsvReader, ProcessableFiles, Task> DetermineFileProcessorActor(string vendor)
        {
            return vendor.Lower() switch
            {
                "eleadtrm" => ProcessTrmFile,
                "ddc" => ProcessDDCFile,
                "edmunds" => ProcessEdmundsFile,
                "unitcounts" => ProcessUnitCountFile,
                "carnow" => ProcessCarNowFile,
                "gubagoo" => ProcessGubagooFile,
                "seo" => ProcessSEOFile,
                _ => null
            };
        }
    }
}