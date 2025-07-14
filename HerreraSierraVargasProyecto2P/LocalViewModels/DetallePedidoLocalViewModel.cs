using System.Collections.ObjectModel;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Models;
using HerreraSierraVargasProyecto2P.Repositories;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class DetallePedidoLocalViewModel : INotifyPropertyChanged
    {
        private readonly AppDatabase _db;
        private readonly LogService _logService;

        public ObservableCollection<DetallePedidoDto> Items { get; } = new();

        private DetallePedidoDto _nuevo = new DetallePedidoDto();
        public DetallePedidoDto Nuevo
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

        public DetallePedidoLocalViewModel(AppDatabase db, LogService logService)
        {
            _db = db;
            _logService = logService;

            CargarCommand = new Command(async () => await Cargar());
            AgregarCommand = new Command(async () => await Agregar());
            EliminarCommand = new Command<DetallePedidoDto>(async (item) => await Eliminar(item));
        }

        public async Task Cargar()
        {
            Items.Clear();
            var lista = await _db.DetallePedidoRepo.GetAllAsync();
            foreach (var item in lista)
                Items.Add(item);
        }

        public async Task Agregar()
        {
            await _db.DetallePedidoRepo.AddAsync(Nuevo);
            await _logService.RegistrarAccionAsync($"Se agregó DetallePedido con ID: {Nuevo.Id}");

            Nuevo = new DetallePedidoDto();
            await Cargar();
        }

        public async Task Eliminar(DetallePedidoDto item)
        {
            await _db.DetallePedidoRepo.DeleteAsync(item);
            await _logService.RegistrarAccionAsync($"Se eliminó DetallePedido con ID: {item.Id}");
            await Cargar();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
    }
}
