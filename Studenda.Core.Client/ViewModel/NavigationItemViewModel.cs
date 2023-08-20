using CommunityToolkit.Mvvm.ComponentModel;
using Studenda.Core.Client.View.AndroidView;

namespace Studenda.Core.Client.ViewModel
{
    public partial class NavigationItemViewModel : ObservableObject
    {
        async public void GoToHome(HomeViewModel homeViewModel)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"vm",homeViewModel }
            };
            await Shell.Current.GoToAsync($"//{nameof(HomeView)}", navigationParameter);
            //?vm={homeViewModel}
        }
        async public void GoToSchedule(HomeViewModel homeViewModel)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"vm",homeViewModel }
            };
            await Shell.Current.GoToAsync($"//{nameof(ScheduleView)}", navigationParameter);
        }

        async public void GoToJournal(HomeViewModel homeViewModel)
        {
            //var navigationParameter = new Dictionary<string, object>
            //{
            //    {"vm",homeViewModel }
            //};
            //await Shell.Current.GoToAsync($"//{nameof(JournalView)}", navigationParameter);
        }
        async public void GoToProfile(HomeViewModel homeViewModel)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"vm",homeViewModel }
            };
            await Shell.Current.GoToAsync($"//{nameof(ProfileView)}", navigationParameter);
        }
        async public void GoToNotifications(HomeViewModel homeViewModel)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"vm",homeViewModel }
            };
            await Shell.Current.GoToAsync($"//{nameof(NotificationView)}", navigationParameter);
        }

        async public void GoToSettings(HomeViewModel homeViewModel)
        {
            //var navigationParameter = new Dictionary<string, object>
            //{
            //    {"vm",homeViewModel }
            //};
            //await Shell.Current.GoToAsync($"//{nameof(SettingView)}", navigationParameter);
        }
    }
}
