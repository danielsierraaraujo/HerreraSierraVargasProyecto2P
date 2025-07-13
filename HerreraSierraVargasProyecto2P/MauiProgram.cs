using System;
using System.Net.Http;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HerreraSierraVargasProyecto2P.Services;
using HerreraSierraVargasProyecto2P.ViewModels;
using HerreraSierraVargasProyecto2P.Views; // <-- Añade esto para registrar tus páginas
using HerreraSierraVargasProyecto2P.Models;

namespace HerreraSierraVargasProyecto2P
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // 1) Registrar HttpClient apuntando al API
            builder.Services.AddHttpClient("InventoryAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7115/");
                // Si tu API no usa HTTPS, cambia por: http://localhost:5034/
            });

            // 2) Registrar servicios (interfaces → implementaciones)
            builder.Services.AddTransient<IProductoService, ProductoService>();
            builder.Services.AddTransient<ICategoriaService, CategoriaService>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<IPedidoService, PedidoService>();
            builder.Services.AddTransient<IDetallePedidoService, DetallePedidoService>();

            // 3) Registrar ViewModels
            builder.Services.AddTransient<ProductosViewModel>();

            // 4) Registrar Vistas (Pages)
            builder.Services.AddTransient<ProductosPage>();

            return builder.Build();
        }
    }
}
