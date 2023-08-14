using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class HomeView : ContentPage
{
    ScheduleViewModel vm = new ScheduleViewModel();
    public HomeView()
    {
        InitializeComponent();
        BindingContext = vm;
    }
}