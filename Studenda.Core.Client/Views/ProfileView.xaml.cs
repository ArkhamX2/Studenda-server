using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

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
}