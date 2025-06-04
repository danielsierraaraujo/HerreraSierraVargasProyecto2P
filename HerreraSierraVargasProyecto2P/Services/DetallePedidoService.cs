using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Inventory.Mobile.Models;
using Microsoft.Extensions.Logging;

namespace Inventory.Mobile.Services
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly HttpClient _http;
        private readonly ILogger<DetallePedidoService> _logger;

        public DetallePedidoService(IHttpClientFactory factory, ILogger<DetallePedidoService> logger)
        {
            _http = factory.CreateClient("InventoryAPI");
            _logger = logger;
        }

        public async Task<List<DetallePedidoDto>> ObtenerTodosAsync()
        {
            var respuesta = await _http.GetAsync("api/DetallePedidos");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<List<DetallePedidoDto>>();
            else
            {
                _logger.LogError("Error al obtener detallePedidos: {StatusCode}", respuesta.StatusCode);
                return new List<DetallePedidoDto>();
            }
        }

        public async Task<DetallePedidoDto> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _http.GetAsync($"api/DetallePedidos/{id}");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<DetallePedidoDto>();
            else
            {
                _logger.LogError("Error al obtener detallePedido {Id}: {StatusCode}", id, respuesta.StatusCode);
                return null;
            }
        }

        public async Task CrearAsync(DetallePedidoDto detalle)
        {
            var contenido = JsonSerializer.Serialize(detalle);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PostAsync("api/DetallePedidos", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al crear detallePedido: {StatusCode}", respuesta.StatusCode);
        }

        public async Task ActualizarAsync(int id, DetallePedidoDto detalle)
        {
            var contenido = JsonSerializer.Serialize(detalle);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PutAsync($"api/DetallePedidos/{id}", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al actualizar detallePedido {Id}: {StatusCode}", id, respuesta.StatusCode);
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _http.DeleteAsync($"api/DetallePedidos/{id}");
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al eliminar detallePedido {Id}: {StatusCode}", id, respuesta.StatusCode);
        }
    }
}
