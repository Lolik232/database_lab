using database_5.ViewModels;

namespace database_5;

public partial class ShopProductList : ContentPage
{

    public ShopProductList(ShopProductListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}