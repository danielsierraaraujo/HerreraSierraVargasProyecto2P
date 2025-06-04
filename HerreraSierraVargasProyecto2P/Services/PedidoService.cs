using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;
using Microsoft.Extensions.Logging;

namespace HerreraSierraVargasProyecto2P.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PedidoService> _logger;

        public PedidoService(IHttpClientFactory factory, ILogger<PedidoService> logger)
        {
            _http = factory.CreateClient("InventoryAPI");
            _logger = logger;
        }

        public async Task<List<PedidoDto>> ObtenerTodosAsync()
        {
            var respuesta = await _http.GetAsync("api/Pedidos");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<List<PedidoDto>>();
            else
            {
                _logger.LogError("Error al obtener pedidos: {StatusCode}", respuesta.StatusCode);
                return new List<PedidoDto>();
            }
        }

        public async Task<PedidoDto> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _http.GetAsync($"api/Pedidos/{id}");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<PedidoDto>();
            else
            {
                _logger.LogError("Error al obtener pedido {Id}: {StatusCode}", id, respuesta.StatusCode);
                return null;
            }
        }

        public async Task<PedidoDto> CrearPedidoAsync(PedidoDto pedido)
        {
            // Serializamos con JsonSerializer para incluir la lista DetallePedidos
            var contenido = JsonSerializer.Serialize(pedido);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PostAsync("api/Pedidos", httpContent);
            if (respuesta.IsSuccessStatusCode)
            {
                // Devuelve el objeto creado (con Id asignado por el servidor)
                return await respuesta.Content.ReadFromJsonAsync<PedidoDto>();
            }
            else
            {
                _logger.LogError("Error al crear pedido: {StatusCode}", respuesta.StatusCode);
                return null;
            }
        }

        public async Task ActualizarAsync(int id, PedidoDto pedido)
        {
            var contenido = JsonSerializer.Serialize(pedido);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PutAsync($"api/Pedidos/{id}", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al actualizar pedido {Id}: {StatusCode}", id, respuesta.StatusCode);
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _http.DeleteAsync($"api/Pedidos/{id}");
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al eliminar pedido {Id}: {StatusCode}", id, respuesta.StatusCode);
        }
    }
}
