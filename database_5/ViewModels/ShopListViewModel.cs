using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.ObjectModel;

namespace database_5.ViewModels
{
    public partial class ShopListViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Shop> _shops = new ObservableCollection<Shop>();

        private readonly IShopRepository _shopRepository;

        public ShopListViewModel(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
            _ = Load();
        }

        [RelayCommand]
        private async Task Load()
        {
            var shops = await _shopRepository.GetAllAsync();
            Shops = new ObservableCollection<Shop>(shops);
        }

        [RelayCommand]
        private async Task ShopSelected(Shop shop)
        {
            await Shell.Current.GoToAsync($"{nameof(ShopProductList)}?", 
                new Dictionary<string, object> {
                    [nameof(Shop)] = shop,
                });
        }

    }
}
