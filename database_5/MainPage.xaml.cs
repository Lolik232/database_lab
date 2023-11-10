using database_5.ViewModels;

namespace database_5
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ShopListViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}