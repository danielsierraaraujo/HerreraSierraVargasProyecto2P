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
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(IHttpClientFactory factory, ILogger<CategoriaService> logger)
        {
            _http = factory.CreateClient("InventoryAPI");
            _logger = logger;
        }

        public async Task<List<CategoriaDto>> ObtenerTodosAsync()
        {
            var respuesta = await _http.GetAsync("api/Categorias");
            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.Content.ReadFromJsonAsync<List<CategoriaDto>>();
            }
            else
            {
                _logger.LogError("Error al obtener categorías: {StatusCode}", respuesta.StatusCode);
                return new List<CategoriaDto>();
            }
        }

        public async Task<CategoriaDto> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _http.GetAsync($"api/Categorias/{id}");
            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.Content.ReadFromJsonAsync<CategoriaDto>();
            }
            else
            {
                _logger.LogError("Error al obtener categoría {Id}: {StatusCode}", id, respuesta.StatusCode);
                return null;
            }
        }

        public async Task CrearAsync(CategoriaDto categoria)
        {
            var contenido = JsonSerializer.Serialize(categoria);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PostAsync("api/Categorias", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al crear categoría: {StatusCode}", respuesta.StatusCode);
        }

        public async Task ActualizarAsync(int id, CategoriaDto categoria)
        {
            var contenido = JsonSerializer.Serialize(categoria);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PutAsync($"api/Categorias/{id}", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al actualizar categoría {Id}: {StatusCode}", id, respuesta.StatusCode);
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _http.DeleteAsync($"api/Categorias/{id}");
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al eliminar categoría {Id}: {StatusCode}", id, respuesta.StatusCode);
        }
    }
}
