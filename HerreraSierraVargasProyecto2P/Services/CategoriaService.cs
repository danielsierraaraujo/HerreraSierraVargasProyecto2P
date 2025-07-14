using System;
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
            try
            {
                var respuesta = await _http.GetAsync("api/Categorias");
                respuesta.EnsureSuccessStatusCode();
                return await respuesta.Content.ReadFromJsonAsync<List<CategoriaDto>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorías");
                return new List<CategoriaDto>();
            }
        }

        public async Task<CategoriaDto> ObtenerPorIdAsync(int id)
        {
            try
            {
                var respuesta = await _http.GetAsync($"api/Categorias/{id}");
                respuesta.EnsureSuccessStatusCode();
                return await respuesta.Content.ReadFromJsonAsync<CategoriaDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categoría por ID");
                return null;
            }
        }

        public async Task<bool> CrearAsync(CategoriaDto categoria)
        {
            try
            {
                var contenido = new StringContent(JsonSerializer.Serialize(categoria), Encoding.UTF8, "application/json");
                var respuesta = await _http.PostAsync("api/Categorias", contenido);

                if (!respuesta.IsSuccessStatusCode)
                {
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error API: {respuesta.StatusCode} - {contenidoError}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear categoría");
                throw;
            }
        }

        public async Task<bool> ActualizarAsync(int id, CategoriaDto categoria)
        {
            try
            {
                var contenido = new StringContent(JsonSerializer.Serialize(categoria), Encoding.UTF8, "application/json");
                var respuesta = await _http.PutAsync($"api/Categorias/{id}", contenido);

                if (!respuesta.IsSuccessStatusCode)
                {
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error API: {respuesta.StatusCode} - {contenidoError}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar categoría");
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var respuesta = await _http.DeleteAsync($"api/Categorias/{id}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    var contenidoError = await respuesta.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error API: {respuesta.StatusCode} - {contenidoError}");
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar categoría");
                throw;
            }
        }
    }
}
