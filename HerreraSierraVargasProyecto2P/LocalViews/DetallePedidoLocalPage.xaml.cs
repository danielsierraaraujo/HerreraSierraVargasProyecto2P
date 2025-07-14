using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;


namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class DetallePedidoLocalPage : ContentPage
{
	public DetallePedidoLocalPage(AppDatabase db, LogService logService)
    {
		InitializeComponent();
        BindingContext = new DetallePedidoLocalViewModel(db, logService);
    }
}