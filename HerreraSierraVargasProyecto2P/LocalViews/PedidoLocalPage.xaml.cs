using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class PedidoLocalPage : ContentPage
{
	public PedidoLocalPage(AppDatabase db, LogService logService)
	{
		InitializeComponent();
        BindingContext = new PedidoLocalViewModel(db, logService);
    }
}