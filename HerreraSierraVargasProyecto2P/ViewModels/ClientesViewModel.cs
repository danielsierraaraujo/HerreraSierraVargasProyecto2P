using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HerreraSierraVargasProyecto2P.ViewModels
{
    public class ClientesViewModel : BindableObject
    {
        private readonly IClienteService _clienteService;

        public ObservableCollection<ClienteDto> Clientes { get; } = new();

        public ICommand LoadClientesCommand { get; }

        public ClientesViewModel(IClienteService clienteService)
        {
            _clienteService = clienteService;
            LoadClientesCommand = new Command(async () => await LoadClientesAsync());

            // Carga inicial
            LoadClientesCommand.Execute(null);
        }

        private async Task LoadClientesAsync()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();

            Clientes.Clear();

            foreach (var cliente in clientes)
                Clientes.Add(cliente);
        }
    }
}
