using System.Collections.ObjectModel;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class CategoriaLocalViewModel : INotifyPropertyChanged
    {
        private readonly AppDatabase _db;
        private readonly LogService _logService;

        public ObservableCollection<CategoriaDto> Items { get; } = new();

        private CategoriaDto _nuevo = new CategoriaDto();
        public CategoriaDto Nuevo
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

        public CategoriaLocalViewModel(AppDatabase db, LogService logService)
        {
            _db = db;
            _logService = logService;

            CargarCommand = new Command(async () => await Cargar());
            AgregarCommand = new Command(async () => await Agregar());
            EliminarCommand = new Command<CategoriaDto>(async (item) => await Eliminar(item));
        }

        public async Task Cargar()
        {
            Items.Clear();
            var lista = await _db.CategoriaRepo.GetAllAsync();
            foreach (var item in lista)
                Items.Add(item);
        }

        public async Task Agregar()
        {
            await _db.CategoriaRepo.AddAsync(Nuevo);
            await _logService.RegistrarAccionAsync($"Se agregó Categoría: {Nuevo.Nombre}");

            Nuevo = new CategoriaDto();
            await Cargar();
        }

        public async Task Eliminar(CategoriaDto item)
        {
            await _db.CategoriaRepo.DeleteAsync(item);
            await _logService.RegistrarAccionAsync($"Se eliminó Categoría: {item.Nombre}");
            await Cargar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
