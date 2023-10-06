using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

public partial class LogInView : ContentPage
{
    LogInViewModel vm = new LogInViewModel();
    public LogInView()
    {
        InitializeComponent();
        BindingContext = vm;

    }
}