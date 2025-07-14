using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HerreraSierraVargasProyecto2P.ViewModels
{
    public class PedidosViewModel : BindableObject
    {
        private readonly IPedidoService _pedidoService;

        public ObservableCollection<PedidoDto> Pedidos { get; } = new();

        private PedidoDto _selectedPedido;
        public PedidoDto SelectedPedido
        {
            get => _selectedPedido;
            set
            {
                if (_selectedPedido != value)
                {
                    _selectedPedido = value;
                    OnPropertyChanged();

                    if (_selectedPedido != null)
                        AbrirDetallePedidoCommand.Execute(_selectedPedido);
                }
            }
        }

        public ICommand LoadPedidosCommand { get; }
        public ICommand AbrirDetallePedidoCommand { get; }
        public ICommand AgregarPedidoCommand { get; }

        public PedidosViewModel(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;

            LoadPedidosCommand = new Command(async () => await LoadPedidosAsync());
            AbrirDetallePedidoCommand = new Command<PedidoDto>(async (pedido) => await AbrirDetallePedidoAsync(pedido));
            AgregarPedidoCommand = new Command(async () => await AgregarPedidoAsync());
        }

        private async Task LoadPedidosAsync()
        {
            var pedidos = await _pedidoService.ObtenerTodosAsync();
            Pedidos.Clear();
            foreach (var p in pedidos)
                Pedidos.Add(p);
        }

        private async Task AbrirDetallePedidoAsync(PedidoDto pedido)
        {
            if (pedido == null) return;
            // Navega a la página detalle con el id del pedido
            await Shell.Current.GoToAsync($"pedidoDetalle?pedidoId={pedido.Id}");
        }

        private async Task AgregarPedidoAsync()
        {
            // Navega a página detalle sin id para crear uno nuevo
            await Shell.Current.GoToAsync("pedidoDetalle");
        }
    }
}
