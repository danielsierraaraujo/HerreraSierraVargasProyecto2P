using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;    // --> ClienteDto
using HerreraSierraVargasProyecto2P.Services;  // --> IClienteService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class ClientesPage : ContentPage
    {
        private readonly IClienteService _clienteService;

        public ClientesPage(IClienteService clienteService)
        {
            InitializeComponent();
            _clienteService = clienteService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar lista de clientes
            var clientes = await _clienteService.ObtenerTodosAsync();
            ClientesListView.ItemsSource = clientes;
        }

        private async void OnClienteSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var clienteSeleccionado = e.CurrentSelection[0] as ClienteDto;
            if (clienteSeleccionado == null)
                return;

            // Navegar a detalle de cliente, enviando el Id como parámetro
            await Shell.Current.GoToAsync($"clienteDetalle?clienteId={clienteSeleccionado.Id}");

            // Deseleccionar item
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnAgregarClienteClicked(object sender, EventArgs e)
        {
            // Navegar a detalle de cliente en modo “nuevo”
            await Shell.Current.GoToAsync("clienteDetalle");
        }
    }
}
