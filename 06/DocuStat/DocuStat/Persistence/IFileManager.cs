using System.Threading.Tasks;

namespace ELTE.DocuStat.Persistence
{
    public interface IFileManager
    {
        string Load();
        Task<string> LoadAsync();
    }
}
