using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.ViewModels;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class ClientesPage : ContentPage
    {
        public ClientesPage(ClientesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnClienteSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var clienteSeleccionado = e.CurrentSelection[0] as ClienteDto;
            if (clienteSeleccionado == null)
                return;

            // Navegar a página de detalle de cliente, pasando parámetro (ajusta ruta si usas otra)
            await Shell.Current.GoToAsync($"clienteDetalle?clienteId={clienteSeleccionado.Id}");

            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarClienteClicked(object sender, System.EventArgs e)
        {
            // Navegar a página para agregar nuevo cliente
            await Shell.Current.GoToAsync("clienteDetalle");
        }
    }
}
