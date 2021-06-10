namespace ETL.DataLoader.Generic.Contracts
{
    public class ProcessableFiles
    {
        public string PathPickup { get; set; }
        public string PathArchive { get; set; }
        public string DirectorySearchPattern { get; set; }
        public string DatabaseConnectionName { get; set; }
        public string DatabaseTableName { get; set; }
        public string PostLoadStoredProcedure { get; set; }
        public string Vendor { get; set; }
    }
}