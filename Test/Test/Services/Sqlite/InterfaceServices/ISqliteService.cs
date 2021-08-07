using System.Threading.Tasks;

namespace Test.Services.Sqlite.InterfaceServices
{
    public interface ISqliteService
    {
        Task CreateDatabaseAsync();
        Task<bool> DBExist();
        Task <bool> DBClear();
    }
}