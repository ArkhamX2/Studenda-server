using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

public partial class HomeView : ContentPage
{
    bool loaded = false;
    HomeViewModel vm = new HomeViewModel();
    public HomeView()
    {
        BindingContext = vm;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!loaded )
            Task.Run(
                async () =>
                {       
                    loaded= true;
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    await TodaySchedule.LoadViewAsync());
                });
    }
}