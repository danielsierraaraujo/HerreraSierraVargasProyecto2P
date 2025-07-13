using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HerreraSierraVargasProyecto2P.Repositories
{
    public class GenericRepository<T> where T : new()
    {
        private readonly SQLiteAsyncConnection _database;

        public GenericRepository(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _database.FindAsync<T>(id);
        }

        public Task<int> AddAsync(T item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> UpdateAsync(T item)
        {
            return _database.UpdateAsync(item);
        }

        public Task<int> DeleteAsync(T item)
        {
            return _database.DeleteAsync(item);
        }

        public Task<int> DeleteByIdAsync(int id)
        {
            return _database.DeleteAsync<T>(id);
        }
    }
}
