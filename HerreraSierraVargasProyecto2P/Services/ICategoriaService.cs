using System.Collections.Generic;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ObtenerTodosAsync();
        Task<CategoriaDto> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(CategoriaDto categoria);              // ← cambiado
        Task<bool> ActualizarAsync(int id, CategoriaDto categoria); // ← cambiado
        Task<bool> EliminarAsync(int id);                           // ← cambiado
    }
}
