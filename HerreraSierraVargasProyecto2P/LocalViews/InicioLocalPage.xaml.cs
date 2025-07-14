using HerreraSierraVargasProyecto2P.LocalViews;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class InicioLocalPage : ContentPage
{
    public InicioLocalPage()
    {
        InitializeComponent();
    }

    private async void OnProductosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProductoLocalPage));
    }

    private async void OnCategoriasClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CategoriaLocalPage));
    }

    private async void OnClientesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClienteLocalPage));
    }

    private async void OnPedidosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PedidoLocalPage));
    }
}
