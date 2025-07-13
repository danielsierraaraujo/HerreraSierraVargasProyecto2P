using System.Collections.ObjectModel;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class ProductoLocalViewModel : INotifyPropertyChanged
    {
        private readonly AppDatabase _db;
        private readonly LogService _logService;

        public ObservableCollection<ProductoDto> Items { get; } = new();

        private ProductoDto _nuevo = new ProductoDto();
        public ProductoDto Nuevo
        {
            get => _nuevo;
            set
            {
                _nuevo = value;
                OnPropertyChanged(nameof(Nuevo));
            }
        }

        public ICommand CargarCommand { get; }
        public ICommand AgregarCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProductoLocalViewModel(AppDatabase db, LogService logService)
        {
            _db = db;
            _logService = logService;

            CargarCommand = new Command(async () => await Cargar());
            AgregarCommand = new Command(async () => await Agregar());
            EliminarCommand = new Command<ProductoDto>(async (item) => await Eliminar(item));
        }

        public async Task Cargar()
        {
            Items.Clear();
            var lista = await _db.ProductoRepo.GetAllAsync();
            foreach (var item in lista)
                Items.Add(item);
        }

        public async Task Agregar()
        {
            await _db.ProductoRepo.AddAsync(Nuevo);
            await _logService.RegistrarAccionAsync($"Se agregó Producto: {Nuevo.Nombre}");

            Nuevo = new ProductoDto();
            await Cargar();
        }

        public async Task Eliminar(ProductoDto item)
        {
            await _db.ProductoRepo.DeleteAsync(item);
            await _logService.RegistrarAccionAsync($"Se eliminó Producto: {item.Nombre}");
            await Cargar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
