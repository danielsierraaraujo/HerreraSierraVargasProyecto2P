using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Mobile.Models;

namespace Inventory.Mobile.Services
{
    public interface IPedidoService
    {
        Task<List<PedidoDto>> ObtenerTodosAsync();
        Task<PedidoDto> ObtenerPorIdAsync(int id);
        Task<PedidoDto> CrearPedidoAsync(PedidoDto pedido);
        Task ActualizarAsync(int id, PedidoDto pedido);
        Task EliminarAsync(int id);
    }
}
