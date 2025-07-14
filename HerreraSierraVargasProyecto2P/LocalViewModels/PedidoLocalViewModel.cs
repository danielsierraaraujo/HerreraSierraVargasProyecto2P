using System.Collections.ObjectModel;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class PedidoLocalViewModel : INotifyPropertyChanged
    {
        private readonly AppDatabase _db;
        private readonly LogService _logService;

        public ObservableCollection<PedidoDto> Items { get; } = new();

        private PedidoDto _nuevo = new PedidoDto();
        public PedidoDto Nuevo
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

        public PedidoLocalViewModel(AppDatabase db, LogService logService)
        {
            _db = db;
            _logService = logService;

            CargarCommand = new Command(async () => await Cargar());
            AgregarCommand = new Command(async () => await Agregar());
            EliminarCommand = new Command<PedidoDto>(async (item) => await Eliminar(item));
        }

        public async Task Cargar()
        {
            Items.Clear();
            var lista = await _db.PedidoRepo.GetAllAsync();
            foreach (var item in lista)
                Items.Add(item);
        }

        public async Task Agregar()
        {
            await _db.PedidoRepo.AddAsync(Nuevo);
            await _logService.RegistrarAccionAsync($"Se agregó Pedido con ID: {Nuevo.Id}");

            Nuevo = new PedidoDto();
            await Cargar();
        }

        public async Task Eliminar(PedidoDto item)
        {
            await _db.PedidoRepo.DeleteAsync(item);
            await _logService.RegistrarAccionAsync($"Se eliminó Pedido con ID: {item.Id}");
            await Cargar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
