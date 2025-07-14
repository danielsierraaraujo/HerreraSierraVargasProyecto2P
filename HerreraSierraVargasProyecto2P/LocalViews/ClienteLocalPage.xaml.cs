using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class ClienteLocalPage : ContentPage
{
	public ClienteLocalPage(AppDatabase db, LogService logService)
	{
		InitializeComponent();
        BindingContext = new ClienteLocalViewModel(db, logService);
    }
}