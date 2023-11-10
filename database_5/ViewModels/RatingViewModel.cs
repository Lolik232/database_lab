using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace database_5.ViewModels
{
    [QueryProperty(nameof(Shop), nameof(Shop))]
    public partial class RatingViewModel : ObservableObject
    {
        [ObservableProperty]
        private Shop _shop;

        [ObservableProperty]
        private ObservableCollection<DAL.Entities.Rating> _shopRating;

        private IShopRepository _shopRepository;

        public RatingViewModel(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Shop))
            {
                ShopRating = new(_shopRepository.GetProductRating((int)Shop.Id));
            }
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
