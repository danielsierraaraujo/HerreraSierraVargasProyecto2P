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
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(IHttpClientFactory factory, ILogger<ProductoService> logger)
        {
            _http = factory.CreateClient("InventoryAPI");
            _logger = logger;
        }

        public async Task<List<ProductoDto>> ObtenerTodosAsync()
        {
            var respuesta = await _http.GetAsync("api/Productos");
            if (respuesta.IsSuccessStatusCode)
            {
                var lista = await respuesta.Content.ReadFromJsonAsync<List<ProductoDto>>();
                return lista;
            }
            else
            {
                _logger.LogError("Error al obtener productos: {StatusCode}", respuesta.StatusCode);
                return new List<ProductoDto>();
            }
        }

        public async Task<ProductoDto> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _http.GetAsync($"api/Productos/{id}");
            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.Content.ReadFromJsonAsync<ProductoDto>();
            }
            else
            {
                _logger.LogError("Error al obtener producto {Id}: {StatusCode}", id, respuesta.StatusCode);
                return null;
            }
        }

        public async Task CrearAsync(ProductoDto producto)
        {
            var contenido = JsonSerializer.Serialize(producto);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PostAsync("api/Productos", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al crear producto: {StatusCode}", respuesta.StatusCode);
        }

        public async Task ActualizarAsync(int id, ProductoDto producto)
        {
            var contenido = JsonSerializer.Serialize(producto);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PutAsync($"api/Productos/{id}", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al actualizar producto {Id}: {StatusCode}", id, respuesta.StatusCode);
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _http.DeleteAsync($"api/Productos/{id}");
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al eliminar producto {Id}: {StatusCode}", id, respuesta.StatusCode);
        }
    }
}