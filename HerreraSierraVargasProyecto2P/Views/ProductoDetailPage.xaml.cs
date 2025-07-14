using System;
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

        private int _productoId;
        public int ProductoId
        {
            get => _productoId;
            set
            {
                _productoId = value;
                // Carga el producto cuando el id se setea desde QueryProperty
                _ = CargarProductoAsync(value);
            }
        }

        private ProductoDto _currentProducto;
        private CategoriaDto[] _categorias;

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

            if (_categorias == null)
            {
                _categorias = (await _categoriaService.ObtenerTodosAsync()).ToArray();
                CategoriaPicker.ItemsSource = _categorias;
                CategoriaPicker.ItemDisplayBinding = new Binding("Nombre");
            }
        }

        private async Task CargarProductoAsync(int productoId)
        {
            if (productoId <= 0)
            {
                _currentProducto = new ProductoDto();
                EliminarButton.IsVisible = false;
                LimpiarCampos();
                return;
            }

            try
            {
                _currentProducto = await _productoService.ObtenerPorIdAsync(productoId);
                if (_currentProducto != null)
                {
                    NombreEntry.Text = _currentProducto.Nombre;
                    DescripcionEditor.Text = _currentProducto.Descripcion;
                    PrecioEntry.Text = _currentProducto.Precio.ToString();
                    StockEntry.Text = _currentProducto.Stock.ToString();

                    var categoriaSeleccionada = _categorias?.FirstOrDefault(c => c.Id == _currentProducto.CategoriaId);
                    if (categoriaSeleccionada != null)
                        CategoriaPicker.SelectedItem = categoriaSeleccionada;

                    EliminarButton.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar el producto: {ex.Message}", "OK");
            }
        }

        private void LimpiarCampos()
        {
            NombreEntry.Text = string.Empty;
            DescripcionEditor.Text = string.Empty;
            PrecioEntry.Text = string.Empty;
            StockEntry.Text = string.Empty;
            CategoriaPicker.SelectedItem = null;
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validaciones b�sicas
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }
            if (!decimal.TryParse(PrecioEntry.Text, out var precio))
            {
                await DisplayAlert("Error", "Precio inv�lido.", "OK");
                return;
            }
            if (!int.TryParse(StockEntry.Text, out var stock))
            {
                await DisplayAlert("Error", "Stock inv�lido.", "OK");
                return;
            }
            if (CategoriaPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Seleccione una categor�a.", "OK");
                return;
            }

            _currentProducto.Nombre = NombreEntry.Text;
            _currentProducto.Descripcion = DescripcionEditor.Text;
            _currentProducto.Precio = precio;
            _currentProducto.Stock = stock;
            _currentProducto.CategoriaId = (CategoriaPicker.SelectedItem as CategoriaDto).Id;

            try
            {
                if (_currentProducto.Id > 0)
                {
                    await _productoService.ActualizarAsync(_currentProducto.Id, _currentProducto);
                }
                else
                {
                    await _productoService.CrearAsync(_currentProducto);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar el producto: {ex.Message}", "OK");
            }
        }

        private async void OnEliminarClicked(object sender, EventArgs e)
        {
            bool confirma = await DisplayAlert("Confirmar", "�Eliminar este producto?", "S�", "No");
            if (!confirma)
                return;

            try
            {
                await _productoService.EliminarAsync(_currentProducto.Id);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo eliminar el producto: {ex.Message}", "OK");
            }
        }
    }
}
