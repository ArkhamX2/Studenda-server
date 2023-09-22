using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

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