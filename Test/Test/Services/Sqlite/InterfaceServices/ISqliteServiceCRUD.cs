using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Services.Sqlite.InterfaceServices
{
    public interface ISqliteServiceCRUD<T>
    {
        Task<List<T>> GetAll();
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Remove(T item);
        Task<T> GetSingle(int Id);
        Task<T> GetSingleByName(string Name);
        Task<T> FirstOrDefault();
    }
}