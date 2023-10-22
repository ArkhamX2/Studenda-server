using CommunityToolkit.Mvvm.Messaging;
using Studenda.Core.Client.Utils;
using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

[QueryProperty(nameof(ViewModel), "vm")]
public partial class ScheduleView : ContentPage
{
    bool loaded = false;
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
    public ScheduleView()
    {
        InitializeComponent();
        BindingContext = ViewModel;
//        WeakReferenceMessenger.Default.Register<ScheduleView, ReloadScheduleMessenger>(
//this,
//async (recipient, message) =>
//{
//    await recipient.Dispatcher.DispatchAsync(
//        async () =>
//        {
//            loaded = false;
//            recipient.OnAppearing();
//        });
//});
    }
    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!loaded)
        Task.Run(
            async () =>
            {
                loaded = true;
                await MainThread.InvokeOnMainThreadAsync(async () => 
                await Schedule.LoadViewAsync());
            });
    }

    private void Button_Clicked(object sender, EventArgs e)
    {        
        loaded = false;
        OnAppearing();
    }
}