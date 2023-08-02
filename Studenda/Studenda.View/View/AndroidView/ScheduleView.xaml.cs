using Studenda.ViewModel;

namespace Studenda.View.AndroidView;

public partial class ScheduleView : ContentPage
{
    ScheduleViewModel vm = new ScheduleViewModel();
    public ScheduleView()
    {
        InitializeComponent();

        BindingContext = vm;
    }
    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}