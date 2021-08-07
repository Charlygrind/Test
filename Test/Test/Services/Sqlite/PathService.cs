using Test.Services.Sqlite.InterfaceServices;
using System.IO;
using MsProspeccion.Services.Sqlite;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace MsProspeccion.Services.Sqlite
{
    public class PathService : IPathService
    {
        public string GetDatabasePath(string DBName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, DBName);
        }
    }
}