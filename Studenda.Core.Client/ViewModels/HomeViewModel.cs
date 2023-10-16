using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModels
{
    public partial class HomeViewModel:ObservableObject
    {
        [ObservableProperty]
        private ScheduleViewModel scheduleViewModel;

        [ObservableProperty]
        private NotificationViewModel notificationViewModel;

        [ObservableProperty]
        private ProfileViewModel profileViewModel;

        [ObservableProperty]
        private JournalViewModel journalViewModel;

        [ObservableProperty]
        private NavigationItemViewModel navigationItemViewModel;

        [ObservableProperty]
        private NavigationBarViewModel navigationBarViewModel;

        [ObservableProperty]
        private ExamViewModel examViewModel;

        [ObservableProperty]
        private GroupSelectorViewModel groupSelectorViewModel;

        public HomeViewModel() 
        {
            InitializeViewModels();
            InitializeModels();
        }

        private void InitializeViewModels()
        {
            ScheduleViewModel = new ScheduleViewModel();
            NotificationViewModel = new NotificationViewModel();
            ProfileViewModel = new ProfileViewModel();
            JournalViewModel = new JournalViewModel();
            NavigationBarViewModel = new NavigationBarViewModel();
            NavigationItemViewModel = new NavigationItemViewModel();
            ExamViewModel = new ExamViewModel();
            GroupSelectorViewModel = new GroupSelectorViewModel();
        }

        private void InitializeModels()
        {
            //Добавить инициализацию модели и распихивание её по вьюмоделам
        }

        [RelayCommand]
        async private void GoToGroupSelectorView()
        {
            await Shell.Current.GoToAsync($"{nameof(GroupSelectorView)}");
        }

        [RelayCommand]
        private void GroupSelector()
        {
            try
            {
                GoToGroupSelectorView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
