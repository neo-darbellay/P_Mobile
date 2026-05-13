using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.data
{
    public class DatabaseContext : IAsyncDisposable
    {
        /// <summary>
        /// The name of the database
        /// </summary>
        private const string Dbname = "db_taskmanager.db3"; //extension à revoir .db2 ou .db3 ou pas du tout
        /// <summary>
        /// The path of the database, static so every instance of DatabaseContext share the same DbPath (and update the same)
        /// </summary>
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, Dbname);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database =>
            (_connection ??= new SQLiteAsyncConnection(DbPath, //??= means that a value is assigned only if null
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));


        /// <summary>
        /// Creates a new table for the class of TTable
        /// </summary>
        /// <typeparam name="TTable">The class we want to create a table for</typeparam>
        /// <returns>Nothing</returns>
        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        /// <summary>
        /// Retrieve the entire table from the database
        /// </summary>
        /// <typeparam name="TTable">The targeted table/class</typeparam>
        /// <returns>The table</returns>
        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database.Table<TTable>();
        }

        /// <summary>
        /// Permit to retrieve records from any table, generate the table if not already here
        /// </summary>
        /// <typeparam name="TTable">Is the table of the class (for example : Task)</typeparam>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        /// <summary>
        /// Retrieves an item based on it's id, also generates the table if not existing
        /// </summary>
        /// <typeparam name="TTable">The table in which we'll search</typeparam>
        /// <param name="primaryKey">The id of the item</param>
        /// <returns>The item</returns>
        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.GetAsync<TTable>(primaryKey);
        }

        /// <summary>
        /// Permit to retrieve some records corresponding to the where clause
        /// </summary>
        /// <typeparam name="TTable">Is the table of the class (for example : Task)</typeparam>
        /// <param name="predicate">A filtering condition clause</param>
        /// <returns>A list of records</returns>
        public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Insert an item into the table, also creates the table if not existing
        /// </summary>
        /// <typeparam name="TTable">The target table</typeparam>
        /// <param name="item">The new item to insert</param>
        /// <returns>True if the operation was successful, else false</returns>
        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            int numberOfRowsAdded = await Database.InsertAsync(item);
            //debug
            Trace.WriteLine("Ajout de " + numberOfRowsAdded.ToString() + " données dans la base de données");
            return numberOfRowsAdded > 0;
        }

        /// <summary>
        /// Updates the targeted item
        /// </summary>
        /// <typeparam name="TTable">The table in which the item figures</typeparam>
        /// <param name="item">The new item</param>
        /// <returns>True if the operation was successful, else false</returns>
        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }

        /// <summary>
        /// Deletes the target item, also creates the table if not existing
        /// </summary>
        /// <typeparam name="TTable">The target table</typeparam>
        /// <param name="item">The item to delete</param>
        /// <returns>True if the operation was successful, else false</returns>
        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync(item) > 0;
        }

        /// <summary>
        /// Deletes the target item based on it's id, also creates the table if not existing
        /// </summary>
        /// <typeparam name="TTable">The target table</typeparam>
        /// <param name="primaryKey">The id of the item to delete</param>
        /// <returns>True if the operation was successful, else false</returns>
        public async Task<bool> DeleteItemByIdAsync<TTable>(object primaryKey) where TTable : class, new() {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

    }
}
