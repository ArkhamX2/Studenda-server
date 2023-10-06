using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

[QueryProperty(nameof(ViewModel), "vm")]
public partial class GroupSelectorView : ContentPage
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
    public GroupSelectorView()
    {
        InitializeComponent();

        BindingContext = ViewModel;
    }
}