using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;    // --> Aquí está CategoriaDto
using HerreraSierraVargasProyecto2P.Services;  // --> Aquí está ICategoriaService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class CategoriasPage : ContentPage
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasPage(ICategoriaService categoriaService)
        {
            InitializeComponent();
            _categoriaService = categoriaService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar lista de categorías
            var categorias = await _categoriaService.ObtenerTodosAsync();
            CategoriasListView.ItemsSource = categorias;
        }

        private async void OnCategoriaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var categoriaSeleccionada = e.CurrentSelection[0] as CategoriaDto;
            if (categoriaSeleccionada == null)
                return;

            // Navegar a detalle de categoría, enviando el Id como parámetro
            await Shell.Current.GoToAsync($"categoriaDetalle?categoriaId={categoriaSeleccionada.Id}");

            // Deseleccionar el item
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarCategoriaClicked(object sender, EventArgs e)
        {
            // Navegar a detalle de categoría en modo “nuevo”
            await Shell.Current.GoToAsync("categoriaDetalle");
        }
    }
}
