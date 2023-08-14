using CommunityToolkit.Mvvm.ComponentModel;
using Studenda.Core.Client.View.AndroidView;

namespace Studenda.Core.Client.ViewModel
{
    public partial class NavigationItemViewModel : ObservableObject
    {
        async public void GoToHome()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
        }
        async public void GoToSchedule()
        {
            await Shell.Current.GoToAsync($"//{nameof(ScheduleView)}");
        }

        async public void GoToJournal()
        {
            //await Shell.Current.GoToAsync($"//{nameof(JournalView)}");
        }
        async public void GoToProfile()
        {
            await Shell.Current.GoToAsync($"//{nameof(ProfileView)}");
        }
        async public void GoToNotifications()
        {
            await Shell.Current.GoToAsync($"//{nameof(NotificationView)}");
        }

        async public void GoToSettings()
        {
            //await Shell.Current.GoToAsync($"//{nameof(SettingView)}");
        }
    }
}
