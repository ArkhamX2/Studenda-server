using Studenda.ViewModel;

namespace Studenda.View.AndroidView;

public partial class SignUpView : ContentPage
{
    SignUpViewModel vm = new SignUpViewModel();

    public SignUpView()
    {
        InitializeComponent();

        BindingContext = vm;
    }
}