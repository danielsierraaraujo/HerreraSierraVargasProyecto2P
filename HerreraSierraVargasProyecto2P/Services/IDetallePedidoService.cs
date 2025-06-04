using System.Collections.Generic;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P.Services
{
    public interface IDetallePedidoService
    {
        Task<List<DetallePedidoDto>> ObtenerTodosAsync();
        Task<DetallePedidoDto> ObtenerPorIdAsync(int id);
        Task CrearAsync(DetallePedidoDto detalle);
        Task ActualizarAsync(int id, DetallePedidoDto detalle);
        Task EliminarAsync(int id);
    }
}