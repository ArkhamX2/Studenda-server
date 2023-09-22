using Studenda.Core.Client.View.AndroidView;
using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client;

public partial class AppShell
{
    private readonly AppShellViewModel _vm = new();

    public AppShell()
    {
        InitializeComponent();

        BindingContext = _vm;

        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
        Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
        Routing.RegisterRoute(nameof(LogInView), typeof(LogInView));
        Routing.RegisterRoute(nameof(JournalView), typeof(JournalView));
        Routing.RegisterRoute(nameof(VerificationView), typeof(VerificationView));
        Routing.RegisterRoute(nameof(GroupSelectorView), typeof(GroupSelectorView));
    }
}