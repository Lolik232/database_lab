using database_5.ViewModels;

namespace database_5;

public partial class Rating : ContentPage
{
    public Rating(RatingViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}