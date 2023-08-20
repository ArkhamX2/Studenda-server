using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModel
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
        }

        private void InitializeModels()
        {
            //Добавить инициализацию модели и распихивание её по вьюмоделам
        }
    }
}
