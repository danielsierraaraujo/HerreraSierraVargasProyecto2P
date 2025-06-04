using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;    // --> PedidoDto
using HerreraSierraVargasProyecto2P.Services;  // --> IPedidoService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class PedidosPage : ContentPage
    {
        private readonly IPedidoService _pedidoService;

        public PedidosPage(IPedidoService pedidoService)
        {
            InitializeComponent();
            _pedidoService = pedidoService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar lista de pedidos
            var pedidos = await _pedidoService.ObtenerTodosAsync();
            PedidosListView.ItemsSource = pedidos;
        }

        private async void OnPedidoSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var pedidoSeleccionado = e.CurrentSelection[0] as PedidoDto;
            if (pedidoSeleccionado == null)
                return;

            // Navegar a detalle de pedido, enviando el Id
            await Shell.Current.GoToAsync($"pedidoDetalle?pedidoId={pedidoSeleccionado.Id}");

            // Deseleccionar item
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarPedidoClicked(object sender, EventArgs e)
        {
            // Navegar a detalle en modo “nuevo”
            await Shell.Current.GoToAsync("pedidoDetalle");
        }
    }
}
