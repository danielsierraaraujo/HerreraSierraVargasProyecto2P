using SQLite;
using System.IO;
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P.Repositories
{
    public class AppDatabase
    {
        private static SQLiteAsyncConnection _database;

        public GenericRepository<ProductoDto> ProductoRepo { get; }
        public GenericRepository<ClienteDto> ClienteRepo { get; }
        public GenericRepository<CategoriaDto> CategoriaRepo { get; }
        public GenericRepository<PedidoDto> PedidoRepo { get; }
        public GenericRepository<DetallePedidoDto> DetallePedidoRepo { get; }

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            ProductoRepo = new GenericRepository<ProductoDto>(_database);
            ClienteRepo = new GenericRepository<ClienteDto>(_database);
            CategoriaRepo = new GenericRepository<CategoriaDto>(_database);
            PedidoRepo = new GenericRepository<PedidoDto>(_database);
            DetallePedidoRepo = new GenericRepository<DetallePedidoDto>(_database);
        }
    }
}
