using Microsoft.Maui.Controls;
using System;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();
        }

        private async void Productos_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("productos");
        }

        private async void Categorias_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("categorias");
        }

        private async void Clientes_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("clientes");
        }

        private async void Pedidos_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("pedidos");
        }
    }
}



