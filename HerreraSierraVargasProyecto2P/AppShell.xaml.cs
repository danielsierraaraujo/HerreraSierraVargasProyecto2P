using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace HerreraSierraVargasProyecto2P
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            this.Navigated += OnShellNavigated;

            // Navegar a Inicio solo al arrancar
            Task.Run(async () => await GoToAsync("//inicio"));
        }

        private async void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            var current = e?.Current?.Location?.ToString();
            var previous = e?.Previous?.Location?.ToString();

            if (current?.Contains("//inicio") == true && previous != null && !previous.Contains("//inicio"))
            {
                // Esperar 100ms para evitar conflicto de navegación en curso
                await Task.Delay(100);

                // Limpiar la pila y volver a la raíz de "inicio"
                await Shell.Current.GoToAsync("//inicio", true);
            }
        }
    }
}
