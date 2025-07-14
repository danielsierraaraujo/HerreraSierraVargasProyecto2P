using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.ViewModels;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class CategoriasPage : ContentPage
    {
        public CategoriasPage(CategoriasViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnCategoriaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var categoriaSeleccionada = e.CurrentSelection[0] as CategoriaDto;
            if (categoriaSeleccionada == null)
                return;

            // Navegar a página de detalle de categoría enviando parámetro
            await Shell.Current.GoToAsync($"categoriaDetalle?categoriaId={categoriaSeleccionada.Id}");

            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarCategoriaClicked(object sender, System.EventArgs e)
        {
            // Navegar a página para crear nueva categoría
            await Shell.Current.GoToAsync("categoriaDetalle");
        }
    }
}
