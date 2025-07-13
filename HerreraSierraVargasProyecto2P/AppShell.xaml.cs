using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas para navegación apilada
            Routing.RegisterRoute("productos", typeof(Views.ProductosPage));
            Routing.RegisterRoute("categorias", typeof(Views.CategoriasPage));
            Routing.RegisterRoute("clientes", typeof(Views.ClientesPage));
            Routing.RegisterRoute("pedidos", typeof(Views.PedidosPage));
            Routing.RegisterRoute("productoDetalle", typeof(Views.ProductoDetailPage));
            Routing.RegisterRoute("categoriaDetalle", typeof(Views.CategoriaDetailPage));
            Routing.RegisterRoute("clienteDetalle", typeof(Views.ClienteDetailPage));
            Routing.RegisterRoute("pedidoDetalle", typeof(Views.PedidoDetailPage));

            // Ir a la pantalla de inicio al lanzar la app
            GoToAsync("//inicio");
        }
    }
}

