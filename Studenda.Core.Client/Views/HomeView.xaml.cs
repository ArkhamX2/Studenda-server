using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class HomeView : ContentPage
{
    HomeViewModel vm = new HomeViewModel();
    public HomeView()
    {
        BindingContext = vm;
        InitializeComponent();
    }
}