using Microsoft.Maui.Controls;
using HerreraSierraVargasProyecto2P.Views;
using HerreraSierraVargasProyecto2P.LocalViews;

namespace HerreraSierraVargasProyecto2P
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("productos", typeof(ProductosPage));
            Routing.RegisterRoute("categorias", typeof(CategoriasPage));
            Routing.RegisterRoute("clientes", typeof(ClientesPage));
            Routing.RegisterRoute("pedidos", typeof(PedidosPage));

            Routing.RegisterRoute("productoDetalle", typeof(ProductoDetailPage));
            Routing.RegisterRoute("categoriaDetalle", typeof(CategoriaDetailPage));
            Routing.RegisterRoute("clienteDetalle", typeof(ClienteDetailPage));
            Routing.RegisterRoute("pedidoDetalle", typeof(PedidoDetailPage));

            Routing.RegisterRoute(nameof(ProductoLocalPage), typeof(ProductoLocalPage));
            Routing.RegisterRoute(nameof(CategoriaLocalPage), typeof(CategoriaLocalPage));
            Routing.RegisterRoute(nameof(ClienteLocalPage), typeof(ClienteLocalPage));
            Routing.RegisterRoute(nameof(PedidoLocalPage), typeof(PedidoLocalPage));
            Routing.RegisterRoute(nameof(LogsPage), typeof(LogsPage));


            Dispatcher.Dispatch(async () =>
            {
                await Shell.Current.GoToAsync("//inicio");
            });
        }
    }
}
