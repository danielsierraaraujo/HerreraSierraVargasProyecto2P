using System;
using System.Net.Http;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Mobile.Services;
using HerreraSierraVargasProyecto2P;

namespace Inventory.Mobile
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
                client.BaseAddress = new Uri("https://localhost:5001/");
                // Ajusta el puerto si tu API corre en otro. 
                // Si tu API no usa HTTPS, usa http://localhost:5000/ o quien sea.
            });

            // 2) Registrar los servicios de consumo (interfaces y clases)
            builder.Services.AddTransient<IProductoService, ProductoService>();
            builder.Services.AddTransient<ICategoriaService, CategoriaService>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<IPedidoService, PedidoService>();
            builder.Services.AddTransient<IDetallePedidoService, DetallePedidoService>();

            return builder.Build();
        }
    }
}
