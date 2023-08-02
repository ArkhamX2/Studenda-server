using Studenda.ViewModel;

namespace Studenda.View.AndroidView;

public partial class ProfileView : ContentPage
{
    ProfileViewModel vm = new ProfileViewModel();
    public ProfileView()
    {
        InitializeComponent();

        BindingContext = vm;
    }
    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}