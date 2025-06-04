using System;
using System.Collections.ObjectModel;
using System.Linq;
using HerreraSierraVargasProyecto2P.Models;    // PedidoDto, DetallePedidoDto, ClienteDto, ProductoDto
using HerreraSierraVargasProyecto2P.Services;  // IPedidoService, IClienteService, IProductoService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    [QueryProperty(nameof(PedidoId), "pedidoId")]
    public partial class PedidoDetailPage : ContentPage
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IProductoService _productoService;

        public int PedidoId { get; set; }

        public ObservableCollection<DetallePedidoDto> DetallePedidos { get; set; }
        public ObservableCollection<ClienteDto> Clientes { get; set; }
        public ObservableCollection<ProductoDto> Productos { get; set; }

        private PedidoDto _currentPedido;

        public PedidoDetailPage(
            IPedidoService pedidoService,
            IClienteService clienteService,
            IProductoService productoService)
        {
            InitializeComponent();

            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _productoService = productoService;

            DetallePedidos = new ObservableCollection<DetallePedidoDto>();
            Clientes = new ObservableCollection<ClienteDto>();
            Productos = new ObservableCollection<ProductoDto>();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // 1) Llenar Clientes
            var listaClientes = await _clienteService.ObtenerTodosAsync();
            Clientes.Clear();
            foreach (var c in listaClientes)
                Clientes.Add(c);

            // 2) Llenar Productos
            var listaProductos = await _productoService.ObtenerTodosAsync();
            Productos.Clear();
            foreach (var p in listaProductos)
                Productos.Add(p);

            // 3) Cargar datos si es edición
            if (PedidoId > 0)
            {
                _currentPedido = await _pedidoService.ObtenerPorIdAsync(PedidoId);
                if (_currentPedido != null)
                {
                    var clienteSel = Clientes.FirstOrDefault(c => c.Id == _currentPedido.ClienteId);
                    if (clienteSel != null)
                        ClientePicker.SelectedItem = clienteSel;

                    DetallePedidos.Clear();
                    foreach (var detalle in _currentPedido.DetallePedidos)
                        DetallePedidos.Add(detalle);
                }
            }
            else
            {
                _currentPedido = new PedidoDto
                {
                    DetallePedidos = new System.Collections.Generic.List<DetallePedidoDto>()
                };
                DetallePedidos.Clear();
            }

            DetallePedidosListView.ItemsSource = DetallePedidos;
        }

        private void OnAgregarItemDetalleClicked(object sender, EventArgs e)
        {
            var nuevoDetalle = new DetallePedidoDto
            {
                Cantidad = 1,
                PrecioUnitario = 0m
            };
            DetallePedidos.Add(nuevoDetalle);
        }

        private void OnEliminarItemDetalleClicked(object sender, EventArgs e)
        {
            var detalleAEliminar = (sender as Button)?.CommandParameter as DetallePedidoDto;
            if (detalleAEliminar != null)
                DetallePedidos.Remove(detalleAEliminar);
        }

        private async void OnGuardarPedidoClicked(object sender, EventArgs e)
        {
            if (ClientePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Seleccione un cliente.", "OK");
                return;
            }

            var clienteSeleccionado = ClientePicker.SelectedItem as ClienteDto;
            _currentPedido.ClienteId = clienteSeleccionado.Id;
            _currentPedido.Fecha = DateTime.UtcNow;
            _currentPedido.DetallePedidos = DetallePedidos.ToList();

            decimal total = 0m;
            foreach (var det in _currentPedido.DetallePedidos)
            {
                if (det.Producto == null)
                {
                    await DisplayAlert("Error", "Cada detalle debe tener un producto.", "OK");
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

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
