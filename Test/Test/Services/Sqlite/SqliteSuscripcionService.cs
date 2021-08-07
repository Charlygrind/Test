using Test.Models;
using Test.Services.Sqlite.InterfaceServices;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Test.Helpers;

namespace Test.Services.Sqlite
{
    public class SqliteSuscripcionService : ISqliteServiceCRUD<Suscripcion>
    {
        private static readonly AsyncLock Mutex = new AsyncLock();
        private readonly SQLiteAsyncConnection _sqlCon;

        public SqliteSuscripcionService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath("Suscripcion.db3");
            _sqlCon = new SQLiteAsyncConnection(databasePath);
        }

        public async Task<List<Suscripcion>> GetAll()
        {
            var items = new List<Suscripcion>();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                items = await _sqlCon.GetAllWithChildrenAsync<Suscripcion>().ConfigureAwait(false);
                //items = await _sqlCon.Table<Cuestionario>().ToListAsync().ConfigureAwait(false);
            }

            return items;
        }
        public async Task<Suscripcion> FirstOrDefault()
        {
            var item = new Suscripcion();
            try
            {
                using (await Mutex.LockAsync().ConfigureAwait(false))
                {
                    item = await _sqlCon.Table<Suscripcion>().FirstOrDefaultAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                var err = e.Message.ToString();
            }
            return item;
        }
        public async Task<bool> Insert(Suscripcion item)
        {
            try
            {
                using (await Mutex.LockAsync().ConfigureAwait(false))
                {
                    //var existingCuestionario = await _sqlCon.Table<Cuestionario>().FirstOrDefaultAsync();

                    //if (existingCuestionario == null)
                    //{
                        await _sqlCon.InsertWithChildrenAsync(item,true).ConfigureAwait(false);
                    //}
                }
                return true;
            }
            catch (Exception E)
            {
                return false;
                var Ee = E.Message.ToString();
            }
            
        }
        public async Task<bool> Update(Suscripcion item)
        {
            try
            {
                using (await Mutex.LockAsync().ConfigureAwait(false))
                {
                    var existingCuestionario = await _sqlCon.Table<Suscripcion>().FirstOrDefaultAsync(x => x.Id == item.Id);

                    if (existingCuestionario != null)
                    {
                        await _sqlCon.UpdateAsync(item).ConfigureAwait(false);
                    }
                }
                return true;
            }
            catch (Exception E)
            {
                return false;
                var Ee = E.Message.ToString();
            }
        }
        public async Task<bool> Remove(Suscripcion item)
        {
            try
            {
                using (await Mutex.LockAsync().ConfigureAwait(false))
                {
                    var existingCuestionario = await _sqlCon.Table<Suscripcion>().FirstOrDefaultAsync(x => x.Id == item.Id);

                    if (existingCuestionario != null)
                    {
                        await _sqlCon.DeleteAsync(item, true);
                    }
                }
                return true;
            }
            catch (Exception E)
            {
                return false;
                var Ee = E.Message.ToString();
            }
            
        }
        public async Task<Suscripcion> GetSingle(int Id)
        {
            var item = new Suscripcion();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                item = await _sqlCon.Table<Suscripcion>().FirstOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
            }
            return item;
        }
        public Task<Suscripcion> GetSingleByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}