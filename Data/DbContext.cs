using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AAG.Global.Data;

namespace ETL.DataLoader.Generic.Data
{
    public class DbContext : BaseDbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bugSnag"></param>
        /// <param name="configuration"></param>
        public DbContext(
              IConfiguration configuration
            , ILogger logger
            , Bugsnag.IClient bugSnag
            , string connectionString) : base(configuration, logger, bugSnag, connectionString) { }
    }
}