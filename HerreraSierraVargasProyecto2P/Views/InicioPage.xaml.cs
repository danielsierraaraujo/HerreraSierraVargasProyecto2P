using System;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();

            // Oculta la TabBar en esta página (opcional)
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void Productos_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//productos");
        }

        private async void Categorias_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//categorias");
        }

        private async void Clientes_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//clientes");
        }

        private async void Pedidos_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//pedidos");
        }
    }
}


