using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HerreraSierraVargasProyecto2P.ViewModels
{
    public class PedidoDetailViewModel : BindableObject
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IProductoService _productoService;

        private PedidoDto _currentPedido;

        public ObservableCollection<DetallePedidoDto> DetallePedidos { get; } = new ObservableCollection<DetallePedidoDto>();
        public ObservableCollection<ClienteDto> Clientes { get; } = new ObservableCollection<ClienteDto>();
        public ObservableCollection<ProductoDto> Productos { get; } = new ObservableCollection<ProductoDto>();

        private ClienteDto _clienteSeleccionado;
        public ClienteDto ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set
            {
                if (_clienteSeleccionado != value)
                {
                    _clienteSeleccionado = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _pedidoId;
        public int PedidoId
        {
            get => _pedidoId;
            set
            {
                if (_pedidoId != value)
                {
                    _pedidoId = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AgregarItemDetalleCommand { get; }
        public ICommand EliminarItemDetalleCommand { get; }
        public ICommand GuardarPedidoCommand { get; }
        public ICommand CancelarCommand { get; }

        public PedidoDetailViewModel(IPedidoService pedidoService, IClienteService clienteService, IProductoService productoService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _productoService = productoService;

            AgregarItemDetalleCommand = new Command(AgregarItemDetalle);
            EliminarItemDetalleCommand = new Command<DetallePedidoDto>(EliminarItemDetalle);
            GuardarPedidoCommand = new Command(async () => await GuardarPedidoAsync());
            CancelarCommand = new Command(async () => await CancelarAsync());
        }

        public async Task LoadDataAsync(int pedidoId)
        {
            PedidoId = pedidoId;

            var listaClientes = await _clienteService.ObtenerTodosAsync();
            Clientes.Clear();
            foreach (var c in listaClientes)
                Clientes.Add(c);

            var listaProductos = await _productoService.ObtenerTodosAsync();
            Productos.Clear();
            foreach (var p in listaProductos)
                Productos.Add(p);

            if (pedidoId > 0)
            {
                _currentPedido = await _pedidoService.ObtenerPorIdAsync(pedidoId);
                ClienteSeleccionado = Clientes.FirstOrDefault(c => c.Id == _currentPedido.ClienteId);

                DetallePedidos.Clear();
                foreach (var det in _currentPedido.DetallePedidos)
                    DetallePedidos.Add(det);
            }
            else
            {
                _currentPedido = new PedidoDto
                {
                    DetallePedidos = new System.Collections.Generic.List<DetallePedidoDto>()
                };
                DetallePedidos.Clear();
            }
        }

        private void AgregarItemDetalle()
        {
            DetallePedidos.Add(new DetallePedidoDto
            {
                Cantidad = 1,
                PrecioUnitario = 0m
            });
        }

        private void EliminarItemDetalle(DetallePedidoDto detalle)
        {
            if (detalle != null)
                DetallePedidos.Remove(detalle);
        }

        private async Task GuardarPedidoAsync()
        {
            if (ClienteSeleccionado == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Seleccione un cliente.", "OK");
                return;
            }

            _currentPedido.ClienteId = ClienteSeleccionado.Id;
            _currentPedido.Fecha = System.DateTime.UtcNow;
            _currentPedido.DetallePedidos = DetallePedidos.ToList();

            decimal total = 0m;
            foreach (var det in _currentPedido.DetallePedidos)
            {
                if (det.Producto == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Cada detalle debe tener un producto.", "OK");
                    return;
                }
                total += det.Cantidad * det.PrecioUnitario;
            }
            _currentPedido.Total = total;

            if (_currentPedido.Id > 0)
                await _pedidoService.ActualizarAsync(_currentPedido.Id, _currentPedido);
            else
                await _pedidoService.CrearPedidoAsync(_currentPedido);

            await Shell.Current.GoToAsync("..");
        }

        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
