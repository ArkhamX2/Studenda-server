using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class VerificationView : ContentPage
{
    VerificationViewModel vm = new VerificationViewModel();
    public VerificationView()
	{
		InitializeComponent();

        BindingContext = vm;
    }
}