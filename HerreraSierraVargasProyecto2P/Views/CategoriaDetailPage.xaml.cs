using System;
using System.Collections.Generic;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
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

            // Conectar manualmente el evento Clicked del botón Guardar
            GuardarButton.Clicked += OnGuardarClicked;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (CategoriaId > 0)
            {
                _currentCategoria = await _categoriaService.ObtenerPorIdAsync(CategoriaId);

                if (_currentCategoria != null)
                {
                    NombreEntry.Text = _currentCategoria.Nombre;
                    EliminarButton.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Error", "No se encontró la categoría.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                _currentCategoria = new CategoriaDto();
                EliminarButton.IsVisible = false;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NombreEntry.Text))
                {
                    await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                    return;
                }

                _currentCategoria.Nombre = NombreEntry.Text;

                if (_currentCategoria.Productos == null)
                    _currentCategoria.Productos = new List<ProductoDto>();

                bool exito;

                if (_currentCategoria.Id > 0)
                {
                    exito = await _categoriaService.ActualizarAsync(_currentCategoria.Id, _currentCategoria);
                }
                else
                {
                    exito = await _categoriaService.CrearAsync(_currentCategoria);
                }

                if (exito)
                {
                    await DisplayAlert("Éxito", "Categoría guardada correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la categoría.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Excepción atrapada", ex.Message + "\n" + ex.StackTrace, "OK");
            }
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmar", "¿Eliminar esta categoría?", "Sí", "No");
            if (!confirma)
                return;

            try
            {
                bool exito = await _categoriaService.EliminarAsync(_currentCategoria.Id);

                if (exito)
                {
                    await DisplayAlert("Éxito", "Categoría eliminada correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar la categoría.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error inesperado", ex.Message, "OK");
            }
        }
    }
}