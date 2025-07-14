using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViewModels
{
    public class LogsViewModel : INotifyPropertyChanged
    {
        private readonly LogService _logService;

        public ObservableCollection<string> Logs { get; } = new();

        public ICommand CargarLogsCommand { get; }
        public ICommand BorrarLogsCommand { get; }

        public LogsViewModel(LogService logService)
        {
            _logService = new LogService();

            CargarLogsCommand = new Command(async () => await CargarLogs());
            BorrarLogsCommand = new Command(async () => await BorrarLogs());

            // Carga automática al iniciar
            CargarLogsCommand.Execute(null);
        }

        public async Task CargarLogs()
        {
            Logs.Clear();
            var registros = await _logService.LeerLogsAsync();

            foreach (var linea in registros)
                Logs.Add(linea);
        }

        public async Task BorrarLogs()
        {
            _logService.BorrarLogs();
            Logs.Clear();
            await _logService.RegistrarAccionAsync("Se borró el historial de logs.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propiedad)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}
