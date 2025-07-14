using System.Collections.ObjectModel;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class ClienteLocalViewModel : INotifyPropertyChanged
    {
        private readonly AppDatabase _db;
        private readonly LogService _logService;

        public ObservableCollection<ClienteDto> Items { get; } = new();

        private ClienteDto _nuevo = new ClienteDto();
        public ClienteDto Nuevo
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

        public ClienteLocalViewModel(AppDatabase db, LogService logService)
        {
            _db = db;
            _logService = logService;

            CargarCommand = new Command(async () => await Cargar());
            AgregarCommand = new Command(async () => await Agregar());
            EliminarCommand = new Command<ClienteDto>(async (item) => await Eliminar(item));
        }

        public async Task Cargar()
        {
            Items.Clear();
            var lista = await _db.ClienteRepo.GetAllAsync();
            foreach (var item in lista)
                Items.Add(item);
        }

        public async Task Agregar()
        {
            await _db.ClienteRepo.AddAsync(Nuevo);
            await _logService.RegistrarAccionAsync($"Se agregó Cliente: {Nuevo.Nombre}");

            Nuevo = new ClienteDto();
            await Cargar();
        }

        public async Task Eliminar(ClienteDto item)
        {
            await _db.ClienteRepo.DeleteAsync(item);
            await _logService.RegistrarAccionAsync($"Se eliminó Cliente: {item.Nombre}");
            await Cargar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
