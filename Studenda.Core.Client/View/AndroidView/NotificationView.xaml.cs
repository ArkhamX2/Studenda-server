using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class NotificationView : ContentPage
{
    NotificationViewModel vm = new NotificationViewModel();

    public NotificationView()
    {
        InitializeComponent();

        BindingContext = vm;
    }

    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

}