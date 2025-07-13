using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HerreraSierraVargasProyecto2P.ViewModels
{
    public class CategoriasViewModel : BindableObject
    {
        private readonly ICategoriaService _categoriaService;

        public ObservableCollection<CategoriaDto> Categorias { get; } = new();

        public ICommand LoadCategoriasCommand { get; }

        public CategoriasViewModel(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;

            LoadCategoriasCommand = new Command(async () => await LoadCategoriasAsync());

            // Carga inicial
            LoadCategoriasCommand.Execute(null);
        }

        private async Task LoadCategoriasAsync()
        {
            var categorias = await _categoriaService.ObtenerTodosAsync();

            Categorias.Clear();

            foreach (var categoria in categorias)
                Categorias.Add(categoria);
        }
    }
}
