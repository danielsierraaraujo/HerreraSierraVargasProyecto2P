using System.Collections.Generic;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P.Services
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
