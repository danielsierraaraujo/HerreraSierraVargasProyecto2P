using System.Collections.Generic;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> ObtenerTodosAsync();
        Task<ProductoDto> ObtenerPorIdAsync(int id);
        Task CrearAsync(ProductoDto producto);
        Task ActualizarAsync(int id, ProductoDto producto);
        Task EliminarAsync(int id);
    }
}