using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class LogsPage : ContentPage
{
	public LogsPage(AppDatabase db, LogService logService)
	{
		InitializeComponent();
        BindingContext = new LogsViewModel(logService);
    }
}