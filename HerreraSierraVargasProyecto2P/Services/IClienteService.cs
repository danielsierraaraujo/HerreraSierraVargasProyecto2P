using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Mobile.Models;

namespace Inventory.Mobile.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto> ObtenerPorIdAsync(int id);
        Task CrearAsync(ClienteDto cliente);
        Task ActualizarAsync(int id, ClienteDto cliente);
        Task EliminarAsync(int id);
    }
}
