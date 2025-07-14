using System;
using System.Collections.Generic;
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

        private PedidoDto _pedido;

        public ObservableCollection<DetallePedidoDto> DetallePedidos { get; } = new ObservableCollection<DetallePedidoDto>();

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

        public ICommand GuardarCommand { get; }

        public PedidoDetailViewModel(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;

            GuardarCommand = new Command(async () => await GuardarPedidoAsync());
        }

        public async Task LoadDataAsync(int pedidoId)
        {
            PedidoId = pedidoId;

            if (pedidoId > 0)
            {
                _pedido = await _pedidoService.ObtenerPorIdAsync(pedidoId);

                // Asigna el detalle al ObservableCollection para binding
                DetallePedidos.Clear();
                foreach (var detalle in _pedido.DetallePedidos)
                    DetallePedidos.Add(detalle);

                // Puedes asignar ClienteSeleccionado si tienes lista de clientes
                // Aquí pongo un ejemplo simple, asume que _pedido tiene Cliente cargado
                ClienteSeleccionado = _pedido.Cliente;
            }
            else
            {
                _pedido = new PedidoDto
                {
                    DetallePedidos = new System.Collections.Generic.List<DetallePedidoDto>()
                };
                DetallePedidos.Clear();
                ClienteSeleccionado = null;
            }
        }

        private async Task GuardarPedidoAsync()
        {
            // Actualiza el pedido con los detalles actuales
            _pedido.DetallePedidos = DetallePedidos.ToList();

            // Aquí podrías validar, asignar fecha, cliente, etc.

            if (_pedido.Id > 0)
                await _pedidoService.ActualizarAsync(_pedido.Id, _pedido);
            else
                await _pedidoService.CrearPedidoAsync(_pedido);

            // Puedes lanzar evento o navegación después de guardar
        }
    }
}
