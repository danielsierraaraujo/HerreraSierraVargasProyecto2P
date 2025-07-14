using HerreraSierraVargasProyecto2P.LocalViewModels;
using HerreraSierraVargasProyecto2P.Repositories;

namespace HerreraSierraVargasProyecto2P.LocalViews;

public partial class ProductoLocalPage : ContentPage
{
	public ProductoLocalPage(AppDatabase db, LogService logService)
	{
		InitializeComponent();
        BindingContext = new ProductoLocalViewModel(db, logService);
    }
}