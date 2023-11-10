namespace database_5
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ShopProductList), typeof(ShopProductList));
            Routing.RegisterRoute(nameof(Rating), typeof(Rating));
        }
    }
}