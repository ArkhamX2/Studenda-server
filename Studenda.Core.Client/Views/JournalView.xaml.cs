using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

[QueryProperty(nameof(ViewModel), "vm")]
public partial class JournalView : ContentPage
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
    public JournalView()
	{
		InitializeComponent();

        BindingContext = ViewModel;
    }
}