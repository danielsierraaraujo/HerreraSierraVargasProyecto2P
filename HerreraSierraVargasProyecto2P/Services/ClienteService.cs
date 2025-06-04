using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HerreraSierraVargasProyecto2P.Services;


namespace HerreraSierraVargasProyecto2P.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IHttpClientFactory factory, ILogger<ClienteService> logger)
        {
            _http = factory.CreateClient("InventoryAPI");
            _logger = logger;
        }

        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            var respuesta = await _http.GetAsync("api/Clientes");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<List<ClienteDto>>();
            else
            {
                _logger.LogError("Error al obtener clientes: {StatusCode}", respuesta.StatusCode);
                return new List<ClienteDto>();
            }
        }

        public async Task<ClienteDto> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _http.GetAsync($"api/Clientes/{id}");
            if (respuesta.IsSuccessStatusCode)
                return await respuesta.Content.ReadFromJsonAsync<ClienteDto>();
            else
            {
                _logger.LogError("Error al obtener cliente {Id}: {StatusCode}", id, respuesta.StatusCode);
                return null;
            }
        }

        public async Task CrearAsync(ClienteDto cliente)
        {
            var contenido = JsonSerializer.Serialize(cliente);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PostAsync("api/Clientes", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al crear cliente: {StatusCode}", respuesta.StatusCode);
        }

        public async Task ActualizarAsync(int id, ClienteDto cliente)
        {
            var contenido = JsonSerializer.Serialize(cliente);
            var httpContent = new StringContent(contenido, Encoding.UTF8, "application/json");

            var respuesta = await _http.PutAsync($"api/Clientes/{id}", httpContent);
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al actualizar cliente {Id}: {StatusCode}", id, respuesta.StatusCode);
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _http.DeleteAsync($"api/Clientes/{id}");
            if (!respuesta.IsSuccessStatusCode)
                _logger.LogError("Error al eliminar cliente {Id}: {StatusCode}", id, respuesta.StatusCode);
        }
    }
}
