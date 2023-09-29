using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.View.AndroidView;

public partial class VerificationApproveView : ContentPage
{
	VerificationApproveViewModel vm = new VerificationApproveViewModel();
	public VerificationApproveView()
	{
		BindingContext = vm;
		InitializeComponent();
	}
}