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
            try
            {
                var respuesta = await _http.GetAsync("api/Productos");
                if (respuesta.IsSuccessStatusCode)
                {
                    var lista = await respuesta.Content.ReadFromJsonAsync<List<ProductoDto>>();
                    return lista ?? new List<ProductoDto>();
                }
                else
                {
                    _logger.LogError("Error al obtener productos: {StatusCode}", respuesta.StatusCode);
                    return new List<ProductoDto>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener productos.");
                return new List<ProductoDto>();
            }
        }

        public async Task<ProductoDto> ObtenerPorIdAsync(int id)
        {
            try
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
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener producto {Id}.", id);
                return null;
            }
        }

        public async Task CrearAsync(ProductoDto producto)
        {
            try
            {
                var contenido = JsonSerializer.Serialize(producto);
                var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

                var respuesta = await _http.PostAsync("api/Productos", httpContent);
                if (!respuesta.IsSuccessStatusCode)
                    _logger.LogError("Error al crear producto: {StatusCode}", respuesta.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al crear producto.");
            }
        }

        public async Task ActualizarAsync(int id, ProductoDto producto)
        {
            try
            {
                var contenido = JsonSerializer.Serialize(producto);
                var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

                var respuesta = await _http.PutAsync($"api/Productos/{id}", httpContent);
                if (!respuesta.IsSuccessStatusCode)
                    _logger.LogError("Error al actualizar producto {Id}: {StatusCode}", id, respuesta.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al actualizar producto {Id}.", id);
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var respuesta = await _http.DeleteAsync($"api/Productos/{id}");
                if (!respuesta.IsSuccessStatusCode)
                    _logger.LogError("Error al eliminar producto {Id}: {StatusCode}", id, respuesta.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al eliminar producto {Id}.", id);
            }
        }
    }
}
