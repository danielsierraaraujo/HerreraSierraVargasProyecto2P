using System;
using HerreraSierraVargasProyecto2P.Models;    // --> ClienteDto
using HerreraSierraVargasProyecto2P.Services;  // --> IClienteService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    [QueryProperty(nameof(ClienteId), "clienteId")]
    public partial class ClienteDetailPage : ContentPage
    {
        private readonly IClienteService _clienteService;
        public int ClienteId { get; set; }

        private ClienteDto _currentCliente;

        public ClienteDetailPage(IClienteService clienteService)
        {
            InitializeComponent();
            _clienteService = clienteService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ClienteId > 0)
            {
                // Modo edición: cargar datos
                _currentCliente = await _clienteService.ObtenerPorIdAsync(ClienteId);
                if (_currentCliente != null)
                {
                    NombreEntry.Text = _currentCliente.Nombre;
                    EmailEntry.Text = _currentCliente.Email;
                    TelefonoEntry.Text = _currentCliente.Telefono;
                    EliminarButton.IsVisible = true;
                }
            }
            else
            {
                // Nuevo cliente
                _currentCliente = new ClienteDto();
                EliminarButton.IsVisible = false;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                await DisplayAlert("Error", "El email es obligatorio.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(TelefonoEntry.Text))
            {
                await DisplayAlert("Error", "El teléfono es obligatorio.", "OK");
                return;
            }

            _currentCliente.Nombre = NombreEntry.Text;
            _currentCliente.Email = EmailEntry.Text;
            _currentCliente.Telefono = TelefonoEntry.Text;

            if (_currentCliente.Id > 0)
            {
                // PUT
                await _clienteService.ActualizarAsync(_currentCliente.Id, _currentCliente);
            }
            else
            {
                // POST
                await _clienteService.CrearAsync(_currentCliente);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmar", "¿Eliminar este cliente?", "Sí", "No");
            if (!confirma)
                return;

            await _clienteService.EliminarAsync(_currentCliente.Id);
            await Shell.Current.GoToAsync("..");
        }
    }
}
