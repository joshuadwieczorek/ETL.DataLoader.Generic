using ETL.DataLoader.Generic.Contracts;
using System.Threading.Tasks;

namespace ETL.DataLoader.Generic.Utilities
{
    public interface IProcessProcessableFilesUtility
    {
        Task Process(ProcessableFiles processableFiles);
    }
}