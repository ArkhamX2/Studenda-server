using Studenda.Core.Client.ViewModels;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Client.Views;

public partial class GroupSelectorView : ContentPage
{
    GroupSelectorViewModel _viewModel = new GroupSelectorViewModel();

    public GroupSelectorView()
    {
        InitializeComponent();

        BindingContext = _viewModel;
    }
}