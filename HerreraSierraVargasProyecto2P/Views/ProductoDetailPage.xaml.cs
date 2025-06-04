using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P.Views
{
    [QueryProperty(nameof(ProductoId), "productoId")]
    public partial class ProductoDetailPage : ContentPage
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public int ProductoId { get; set; }  // se llena vía QueryProperty

        private ProductoDto _currentProducto;

        public ProductoDetailPage(IProductoService productoService,
                                 ICategoriaService categoriaService)
        {
            InitializeComponent();

            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // 1) Cargar todas las categorías en el Picker (para asignar producto.CategoriaId)
            var categorias = await _categoriaService.ObtenerTodosAsync();
            CategoriaPicker.ItemsSource = categorias;
            CategoriaPicker.ItemDisplayBinding = new Binding("Nombre");

            // 2) Si ProductoId > 0, estamos en “editar”, si es 0 o no viene, es “nuevo”
            if (ProductoId > 0)
            {
                _currentProducto = await _productoService.ObtenerPorIdAsync(ProductoId);

                if (_currentProducto != null)
                {
                    NombreEntry.Text = _currentProducto.Nombre;
                    DescripcionEditor.Text = _currentProducto.Descripcion;
                    PrecioEntry.Text = _currentProducto.Precio.ToString();
                    StockEntry.Text = _currentProducto.Stock.ToString();

                    // Seleccionar la categoría actual en el Picker
                    var categoriaSeleccionada = categorias.FirstOrDefault(c => c.Id == _currentProducto.CategoriaId);
                    if (categoriaSeleccionada != null)
                        CategoriaPicker.SelectedItem = categoriaSeleccionada;

                    EliminarButton.IsVisible = true; // mostrar botón “Eliminar”
                }
            }
            else
            {
                // Nuevo producto: limpiar campos y ocultar “Eliminar”
                _currentProducto = new ProductoDto();
                EliminarButton.IsVisible = false;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validar (módulo muy básico de validación)
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }
            if (!decimal.TryParse(PrecioEntry.Text, out var precio))
            {
                await DisplayAlert("Error", "Precio inválido.", "OK");
                return;
            }
            if (!int.TryParse(StockEntry.Text, out var stock))
            {
                await DisplayAlert("Error", "Stock inválido.", "OK");
                return;
            }
            if (CategoriaPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Seleccione una categoría.", "OK");
                return;
            }

            // Llenar _currentProducto con datos del formulario
            _currentProducto.Nombre = NombreEntry.Text;
            _currentProducto.Descripcion = DescripcionEditor.Text;
            _currentProducto.Precio = precio;
            _currentProducto.Stock = stock;
            _currentProducto.CategoriaId = (CategoriaPicker.SelectedItem as CategoriaDto).Id;

            // Si viene de edición (_currentProducto.Id > 0), ejecutamos PUT; si no, POST
            if (_currentProducto.Id > 0)
            {
                await _productoService.ActualizarAsync(_currentProducto.Id, _currentProducto);
            }
            else
            {
                await _productoService.CrearAsync(_currentProducto);
            }

            // Una vez guardado, volver a la lista
            await Shell.Current.GoToAsync("..");
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmar", "¿Eliminar este producto?", "Sí", "No");
            if (!confirma)
                return;

            await _productoService.EliminarAsync(_currentProducto.Id);
            await Shell.Current.GoToAsync("..");
        }
    }
}
