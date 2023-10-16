using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

public partial class VerificationView : ContentPage
{
    VerificationViewModel vm = new VerificationViewModel();
    public VerificationView()
	{
		InitializeComponent();

        BindingContext = vm;
    }
}