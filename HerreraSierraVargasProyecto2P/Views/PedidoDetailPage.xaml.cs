using HerreraSierraVargasProyecto2P.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class PedidoDetailPage : ContentPage
    {
        private readonly PedidoDetailViewModel _viewModel;

        public PedidoDetailPage(PedidoDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        // Opcional: Recibir pedidoId si usas Shell routing con QueryProperty
        public int PedidoId { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar datos con el pedidoId
            try
            {
                await _viewModel.LoadDataAsync(PedidoId);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar el pedido: {ex.Message}", "OK");
            }
        }
    }
}
