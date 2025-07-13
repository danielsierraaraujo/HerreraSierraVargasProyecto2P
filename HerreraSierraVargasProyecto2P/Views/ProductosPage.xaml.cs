
using Microsoft.Maui.Controls;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.ViewModels;
namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class ProductosPage : ContentPage
    {
        public ProductosPage(ProductosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnProductoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var productoSeleccionado = e.CurrentSelection[0] as ProductoDto;
            if (productoSeleccionado == null)
                return;

            await Shell.Current.GoToAsync($"productoDetalle?productoId={productoSeleccionado.Id}");

            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarProductoClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("productoDetalle");
        }
    }
}

