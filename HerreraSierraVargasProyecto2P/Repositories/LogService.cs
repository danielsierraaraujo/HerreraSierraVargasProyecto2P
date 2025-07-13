using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace HerreraSierraVargasProyecto2P.Repositories
{
    public class LogService
    {
        private readonly string _logFilePath;

        public LogService(string fileName = "Logs_InventarioSistema.txt")
        {
            _logFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        }

        public async Task RegistrarAccionAsync(string mensaje)
        {
            string log = $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {mensaje}\n";
            await File.AppendAllTextAsync(_logFilePath, log);
        }

        public async Task<string[]> LeerLogsAsync()
        {
            if (!File.Exists(_logFilePath))
                return new string[0];

            return await File.ReadAllLinesAsync(_logFilePath);
        }

        public void BorrarLogs()
        {
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);
        }
    }
}
