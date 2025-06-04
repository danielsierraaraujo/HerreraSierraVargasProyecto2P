using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Mobile.Models;

namespace Inventory.Mobile.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ObtenerTodosAsync();
        Task<CategoriaDto> ObtenerPorIdAsync(int id);
        Task CrearAsync(CategoriaDto categoria);
        Task ActualizarAsync(int id, CategoriaDto categoria);
        Task EliminarAsync(int id);
    }
}
