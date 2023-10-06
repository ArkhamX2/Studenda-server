using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

public partial class HomeView : ContentPage
{
    HomeViewModel vm = new HomeViewModel();
    public HomeView()
    {
        BindingContext = vm;
        InitializeComponent();
    }
}