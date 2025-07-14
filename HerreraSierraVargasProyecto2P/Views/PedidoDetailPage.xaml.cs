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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // Asume que recibes pedidoId de alguna forma, por ejemplo navegación con parámetros
                int pedidoId = 0; // Cambia según contexto real

                await _viewModel.LoadDataAsync(pedidoId);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar el pedido: {ex.Message}", "OK");
            }
        }
    }
}
