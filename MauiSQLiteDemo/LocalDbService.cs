using SQLite;

namespace MauiSQLiteDemo
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            _connection.CreateTableAsync<Clientes>();
        }

        public async Task<List<Clientes>> GetClientes()
        {
            return await _connection.Table<Clientes>().ToListAsync();
        }

        public async Task<Clientes> GetById(int id)
        {
            return await _connection.Table<Clientes>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Clientes clientes)
        {
            await _connection.InsertAsync(clientes);
        }

        public async Task Update(Clientes clientes)
        {
            await _connection.UpdateAsync(clientes);
        }

        public async Task Delete(Clientes clientes)
        {
            await _connection.DeleteAsync(clientes);
        }

    }
}