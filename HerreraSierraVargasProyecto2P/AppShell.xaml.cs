using Microsoft.Maui.Controls;
using HerreraSierraVargasProyecto2P.LocalViews;
using HerreraSierraVargasProyecto2P.Views;


namespace HerreraSierraVargasProyecto2P
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Rutas API
            Routing.RegisterRoute(nameof(ProductosPage), typeof(ProductosPage));
            Routing.RegisterRoute(nameof(CategoriasPage), typeof(CategoriasPage));
            Routing.RegisterRoute(nameof(ClientesPage), typeof(ClientesPage));
            Routing.RegisterRoute(nameof(PedidosPage), typeof(PedidosPage));

            // Rutas SQLite Local
            Routing.RegisterRoute(nameof(ProductoLocalPage), typeof(ProductoLocalPage));
            Routing.RegisterRoute(nameof(CategoriaLocalPage), typeof(CategoriaLocalPage));
            Routing.RegisterRoute(nameof(ClienteLocalPage), typeof(ClienteLocalPage));
            Routing.RegisterRoute(nameof(PedidoLocalPage), typeof(PedidoLocalPage));
        }
    }
}

