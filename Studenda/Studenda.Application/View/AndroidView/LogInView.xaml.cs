using Studenda.ViewModel;

namespace Studenda.View.AndroidView;

public partial class LogInView : ContentPage
{
    LogInViewModel vm = new LogInViewModel();
    public LogInView()
    {
        InitializeComponent();
        BindingContext = vm;

    }
}