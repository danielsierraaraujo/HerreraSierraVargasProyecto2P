using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class CategoriaLocalPage : ContentPage
{
	public CategoriaLocalPage(AppDatabase db, LogService logService)
	{
		InitializeComponent();
        BindingContext = new CategoriaLocalViewModel(db, logService);
    }
}