using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Studenda.Core.Client.Utils;
using Studenda.Core.Client.Views;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Client.ViewModels
{
    public enum WeekType
    {
        Red, Blue
    }

    public enum ScheduleViewType
    {
        Grid, Calendar
    }

    public class Subject
    {
        public string Time { get; set; }
        public string Title { get; set; }
        public string Place { get; set; }

        public Subject(string time, string title, string place)
        {
            Time = time;
            Title = title;
            Place = place;
        }
    }

    public class DaySchedule
    {
        public List<Subject> SubjectList { get; set; }
        public string DayTitle { get; set; }

        public DaySchedule(string dayTitle, List<Subject> subjectList)
        {
            DayTitle = dayTitle;
            SubjectList = subjectList;
        }

    }

    public class WeekSchedule
    {
        public List<DaySchedule> ScheduleList { get; set; }
        public WeekType CurrentWeekType { get; set; }
        public WeekSchedule(List<DaySchedule> scheduleList, WeekType currentWeekType)
        {
            ScheduleList = scheduleList;
            CurrentWeekType = currentWeekType;
        }
    }


    public partial class ScheduleViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<Subject> currentDaySubjectList;

        [ObservableProperty]
        private Group selectedGroup;

        [ObservableProperty]
        private List<Group> groupList;

        [ObservableProperty]
        private ScheduleViewType viewType;

        [ObservableProperty]
        private WeekType typeOfWeek;

        [ObservableProperty]
        private WeekSchedule schedule;

        [ObservableProperty]
        private List<DaySchedule> scheduleList;

        public ScheduleViewModel()
        {
            TypeOfWeek = WeekType.Blue;
            ViewType = ScheduleViewType.Calendar;
            WeekSchedule weekScheduleBlue = new WeekSchedule(
                new List<DaySchedule>()
                {
                    new DaySchedule("Понедельник",new List<Subject>()
                    {
                        new Subject("8.30","Понедельник","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                    }),
                    new DaySchedule("Вторник",new List<Subject>()
                    {
                        new Subject("8.30","Вторник","вц-303"),
                    }),
                    new DaySchedule("Среда",new List<Subject>()
                    {
                        new Subject("8.30","Математика","вц-303"),
                        new Subject("8.30","Среда","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                    }),
                    new DaySchedule("Четверг",new List<Subject>()
                    {
                        new Subject("8.30","Понедельник","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                        new Subject("8.30","Понедельник","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                    }),
                    new DaySchedule("Пятница",new List<Subject>()
                    {
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                    }),
                    new DaySchedule("Суббота",new List<Subject>()
                    {
                        new Subject("8.30","Математика","вц-303"),
                        new Subject("8.30","Среда","вц-303"),
                        new Subject("8.30","Среда","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                    }
                    ),
                }
                , TypeOfWeek);

            WeekSchedule weekScheduleRed = new WeekSchedule(
    new List<DaySchedule>()
    {
                    new DaySchedule("Понедельник",new List<Subject>()
                    {
                        new Subject("8.30","Ерунда","вц-303"),
                        new Subject("8.30","Ерунда","вц-303"),
                    }),

                    new DaySchedule("Среда",new List<Subject>()
                    {
                        new Subject("8.30","Математика","вц-303"),
                    }),
                    new DaySchedule("Четверг",new List<Subject>()
                    {
                        new Subject("8.30","Ерунда","вц-303"),
                        new Subject("8.30","Ерунда","вц-303"),
                    }),
                    new DaySchedule("Пятница",new List<Subject>()
                    {
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                        new Subject("8.30","Вторник","вц-303"),
                    }),
                    new DaySchedule("Суббота",new List<Subject>()
                    {
                        new Subject("8.30","Математика","вц-303"),
                        new Subject("8.30","Среда","вц-303"),
                        new Subject("8.30","Среда","вц-303"),
                        new Subject("8.30","Математика","вц-303"),
                    }
                    ),
    }
    , TypeOfWeek);

            Schedule = weekScheduleBlue;

            ScheduleList = Schedule.ScheduleList;
            CurrentDaySubjectList = ScheduleList[0].SubjectList;
            GroupList = new List<Group>()
            {
               new Group(){ Name="Б.ПИН.РИС 20.06"},
               new Group(){ Name="Б.ПИН.РИС 21.06"},
               new Group() { Name = "Б.ПИН.РИС 22.06" },
            };
            SelectedGroup = GroupList[0];

            WeakReferenceMessenger.Default.Register<ScheduleViewModel, Messenger>(
            this,
            async (recipient, message) =>
            {
                if (Schedule== weekScheduleBlue)
                    Schedule = weekScheduleRed;
                else
                    Schedule = weekScheduleBlue;
                try
                {
                    ScheduleList = Schedule.ScheduleList;
                }
                catch (Exception ex)
                {
                    ScheduleList = null;
                }
                
            });

            //Task.Run(
            //async () =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        GC.Collect();
            //    }
            //});

        }

        [RelayCommand]
        async private void GoToProfileView()
        {
            await Shell.Current.GoToAsync($"{nameof(ProfileView)}");
        }

    }

}
