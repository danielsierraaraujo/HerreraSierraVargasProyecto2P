using System;
using HerreraSierraVargasProyecto2P.Models;    // --> CategoriaDto
using HerreraSierraVargasProyecto2P.Services;  // --> ICategoriaService
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    [QueryProperty(nameof(CategoriaId), "categoriaId")]
    public partial class CategoriaDetailPage : ContentPage
    {
        private readonly ICategoriaService _categoriaService;
        public int CategoriaId { get; set; }

        private CategoriaDto _currentCategoria;

        public CategoriaDetailPage(ICategoriaService categoriaService)
        {
            InitializeComponent();
            _categoriaService = categoriaService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (CategoriaId > 0)
            {
                // Modo edición: cargamos los datos
                _currentCategoria = await _categoriaService.ObtenerPorIdAsync(CategoriaId);
                if (_currentCategoria != null)
                {
                    NombreEntry.Text = _currentCategoria.Nombre;
                    EliminarButton.IsVisible = true;
                }
            }
            else
            {
                // Modo nuevo
                _currentCategoria = new CategoriaDto();
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

            _currentCategoria.Nombre = NombreEntry.Text;

            if (_currentCategoria.Id > 0)
            {
                // PUT
                await _categoriaService.ActualizarAsync(_currentCategoria.Id, _currentCategoria);
            }
            else
            {
                // POST
                await _categoriaService.CrearAsync(_currentCategoria);
            }

            // Volver atrás
            await Shell.Current.GoToAsync("..");
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmar", "¿Eliminar esta categoría?", "Sí", "No");
            if (!confirma)
                return;

            await _categoriaService.EliminarAsync(_currentCategoria.Id);
            await Shell.Current.GoToAsync("..");
        }
    }
}
