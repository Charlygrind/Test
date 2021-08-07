using Test.Helpers;
using Test.Models;
using Test.Services.Sqlite.InterfaceServices;
using SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test.Services.Sqlite
{
    public class SqliteService : ISqliteService
    {
        private static readonly AsyncLock Mutex = new AsyncLock();
        private readonly SQLiteAsyncConnection _sqlCon;

        public SqliteService() {
            string databasePath = DependencyService.Get<IPathService>().GetDatabasePath("Suscripcion.db3");
            _sqlCon = new SQLiteAsyncConnection(databasePath);

        }
        public async Task CreateDatabaseAsync()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                //Register here tables to create
                //if (!TableExist(nameof(Suscripcion)))
                    await _sqlCon.CreateTableAsync<Suscripcion>().ConfigureAwait(false);
            }
        }
        public  async Task<bool> DBExist()
        {
            string databasePath = DependencyService.Get<IPathService>().GetDatabasePath("Suscripcion.db3");
            bool exist = File.Exists(databasePath);
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    if (exist)
            //    {
            //        _sqlCon.CloseAsync();
            //        File.Delete(databasePath);
            //        exist = File.Exists(databasePath);
            //    }
            //}
            await _sqlCon.CloseAsync();

            return exist;
        }
        public async Task<bool> DBClear()
        {
            string databasePath = DependencyService.Get<IPathService>().GetDatabasePath("Suscripcion.db3");
            bool exist = File.Exists(databasePath);
            if (exist)
            {
                var tables = _sqlCon.TableMappings.ToList();
                if (tables.Count > 0)
                    foreach (var table in tables)
                    {
                        if (!table.TableName.Equals("ColumnInfo"))
                        {
                            await _sqlCon.DeleteAllAsync(table);
                            await _sqlCon.DropTableAsync(table);
                        }
                    }
                _sqlCon.CloseAsync().Wait();
                File.Delete(databasePath);
            }
            exist = File.Exists(databasePath);
            return exist;
        }
        public bool TableExist(string Table)
        {
            var res = _sqlCon.TableMappings.FirstOrDefault(x => x.TableName == Table);

            if (res != null)
                return true;
            else
                return false;
        }
    }
}