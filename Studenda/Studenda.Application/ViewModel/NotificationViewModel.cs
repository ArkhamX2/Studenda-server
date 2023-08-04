using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Studenda.ViewModel
{
    public partial class NotificationViewModel : ObservableObject
    {

        #region NotificationClass
        public class Notification
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }

            //TODO: Переделать тип на User
            public string Sender { get; set; }

            public string Description { get; set; }

            public Notification(string name, DateTime date, string sender, string description)
            {
                Name = name;
                Date = date;
                Sender = sender;
                Description = description;
            }
        }
        #endregion

        [ObservableProperty]
        private List<Notification> notificationList = new List<Notification>
        {
            new Notification("Test", DateTime.Now, "Admin", "This is a test notification #1"),
            new Notification("Test", DateTime.Now, "Admin", "This is a test notification #2"),
            new Notification("Test", DateTime.Now, "Admin", "This is a test notification #3"),
        };

        [ObservableProperty]
        private ObservableCollection<Notification> notificationCollection;

        [ObservableProperty]
        private int count;

        //TODO: Возникают ошибки приявзки, но при этом всё отображается корректно. Нужно убрать ошибки привязки
        public NotificationViewModel()
        {
            NotificationCollection = new(NotificationList);
            Count = NotificationCollection.Count;
        }

    }
}
