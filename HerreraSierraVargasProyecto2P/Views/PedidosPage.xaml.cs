using HerreraSierraVargasProyecto2P.ViewModels;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    public partial class PedidosPage : ContentPage
    {
        private readonly PedidosViewModel _viewModel;

        public PedidosPage(PedidosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadPedidosCommand.Execute(null);
        }
    }
}
