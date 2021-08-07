using Test.Models;
using System.Threading.Tasks;

namespace Test.Services.ClientHTTP
{
    public interface IServiceBaseHTTP<T>
    {
        Task<ResultObject<T>> Get();
        Task<ResultList<T>> GetList(string TipoSuscripcion, string Category,int Pge);
        Task<ResultObject<T>> Add();
        Task<ResultObject<T>> Edit();
        Task<ResultObject<T>> Delete();
    }
}