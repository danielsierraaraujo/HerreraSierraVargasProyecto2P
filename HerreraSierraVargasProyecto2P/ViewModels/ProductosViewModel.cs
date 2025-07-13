using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HerreraSierraVargasProyecto2P.ViewModels // <--- ESTA LÍNEA FALTABA
{
    public class ProductosViewModel : BindableObject
    {
        private readonly IProductoService _productoService;

        public ObservableCollection<ProductoDto> Productos { get; } = new();

        public ICommand LoadProductosCommand { get; }

        public ProductosViewModel(IProductoService productoService)
        {
            _productoService = productoService;

            LoadProductosCommand = new Command(async () => await LoadProductosAsync());

            // Carga inicial
            LoadProductosCommand.Execute(null);
        }

        private async Task LoadProductosAsync()
        {
            var productos = await _productoService.ObtenerTodosAsync();

            Productos.Clear();

            foreach (var producto in productos)
                Productos.Add(producto);
        }
    }
}
