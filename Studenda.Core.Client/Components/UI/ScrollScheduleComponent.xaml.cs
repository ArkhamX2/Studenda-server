using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Components.UI;

[QueryProperty(nameof(ViewModel), "vm")]
public partial class ScrollScheduleComponent : ContentView
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
    public ScrollScheduleComponent()
	{
		InitializeComponent();
        BindingContext = ViewModel;
    }
}