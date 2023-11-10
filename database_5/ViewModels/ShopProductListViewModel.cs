using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.ObjectModel;

namespace database_5.ViewModels;

[QueryProperty(nameof(Shop), nameof(Shop))]
public partial class ShopProductListViewModel : ObservableObject
{
    [ObservableProperty]
    private Shop _shop;

    private readonly IShopRepository _shopRepository;

    public ShopProductListViewModel(IShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task GoToRating() {
        await Shell.Current.GoToAsync($"{nameof(Rating)}?",
                new Dictionary<string, object>
                {
                    [nameof(Shop)] = Shop,
                });
    }
}
