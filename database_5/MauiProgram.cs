using DAL.Interfaces;
using DAL.PostgreSQL.EfCore;
using DAL.PostgreSQL.EfCore.Repositories;
using database_5.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace database_5
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services
                .AddDbContext<ChainOfShopsLuckContext>(
                s => s.UseNpgsql("Host=localhost;Port=5432;Database=chain_of_shops_luck;Username=admin;Password=secret"));
            //ChainOfShopsLuckContext ctx = new ChainOfShopsLuckContext();
            //builder.Services.AddSingleton<ChainOfShopsLuckContext>();


            // register view models
            builder.Services.AddSingleton<ShopListViewModel>();
            builder.Services.AddTransient<ShopProductListViewModel>();
            builder.Services.AddTransient<RatingViewModel>();

            // register pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ShopProductList>();

            builder.Services.AddTransient<Rating>();


            builder.Services.AddSingleton<IShopRepository, ShopRepository>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();

            return builder.Build();
        }
    }
}