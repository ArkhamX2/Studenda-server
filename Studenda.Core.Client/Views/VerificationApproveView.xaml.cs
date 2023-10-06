using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Views;

public partial class VerificationApproveView : ContentPage
{
	VerificationApproveViewModel vm = new VerificationApproveViewModel();
	public VerificationApproveView()
	{
		BindingContext = vm;
		InitializeComponent();
	}
}