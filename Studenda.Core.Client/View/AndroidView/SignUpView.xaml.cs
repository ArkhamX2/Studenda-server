using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class SignUpView : ContentPage
{
    SignUpViewModel vm = new SignUpViewModel();

    public SignUpView()
    {
        InitializeComponent();

        BindingContext = vm;
    }
}