using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;    
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class ProductosPage : ContentPage
    {
        private readonly IProductoService _productoService;

        public ProductosPage(IProductoService productoService)
        {
            InitializeComponent();
            _productoService = productoService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cada vez que aparece la página, recargamos la lista de productos
            var productos = await _productoService.ObtenerTodosAsync();
            ProductosListView.ItemsSource = productos;
        }

        private async void OnProductoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var productoSeleccionado = e.CurrentSelection[0] as ProductoDto;
            if (productoSeleccionado == null)
                return;

            // Navegar a página de detalle, enviando el id como parámetro
            await Shell.Current.GoToAsync($"productoDetalle?productoId={productoSeleccionado.Id}");

            // Deseleccionar item
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarProductoClicked(object sender, EventArgs e)
        {
            // Navegar a la misma página de detalle, pero sin parámetro → modo “crear nuevo”
            await Shell.Current.GoToAsync("productoDetalle");
        }
    }
}
