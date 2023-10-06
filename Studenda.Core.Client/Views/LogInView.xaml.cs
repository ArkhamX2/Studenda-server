using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class LogInView : ContentPage
{
    LogInViewModel vm = new LogInViewModel();
    public LogInView()
    {
        InitializeComponent();
        BindingContext = vm;

    }
}