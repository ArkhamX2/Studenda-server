using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

[QueryProperty(nameof(ViewModel), "vm")]
public partial class ProfileView : ContentPage
{
    HomeViewModel _viewModel;
    public HomeViewModel ViewModel
    {
        get => _viewModel;
        set
        {
            _viewModel = value;
            BindingContext = _viewModel;
            OnPropertyChanged();
        }
    }
    public ProfileView()
    {
        InitializeComponent();

        BindingContext = ViewModel;
    }
    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}