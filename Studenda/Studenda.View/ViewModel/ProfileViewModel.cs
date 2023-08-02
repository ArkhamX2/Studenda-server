using CommunityToolkit.Mvvm.ComponentModel;

namespace Studenda.ViewModel
{
    public class Faculty
    {
        public string Name { get; set; }
        public Faculty(string name)
        {
            Name = name;
        }
    }
    public class Course
    {
        public string Name { get; set; }

        public Course(string name)
        {
            Name = name;
        }
    }

    public class Group
    {
        public string Name { get; set; }
        public Group(string name)
        {
            Name = name;
        }
    }

    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string secondName;

        [ObservableProperty]
        private DateTime dateOfBirth;

        [ObservableProperty]
        private Faculty faculty;

        [ObservableProperty]
        private Course course;

        [ObservableProperty]
        private Group group;

        [ObservableProperty]
        private string about;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string vkLink;

        [ObservableProperty]
        private string telegramLink;


        public ProfileViewModel()
        {
            FirstName = "admin";
            secondName = "admin";
            dateOfBirth = DateTime.Now;
            Faculty = new Faculty("ФИТ");
            Course = new Course("3 курс");
            Group = new Group("ПИН2106");
            About = string.Empty;
            PhoneNumber = "+7 913 71 37";
            Email = "test@test.ru";
            vkLink = "vk.com/test";
            TelegramLink = "@telega";
        }
    }
}
