using Studenda.ViewModel;

namespace Studenda.View.AndroidView;

public partial class HomeView : ContentPage
{
	ScheduleViewModel vm = new ScheduleViewModel();
	public HomeView()
	{
		InitializeComponent();
        BindingContext = vm;
    }
}