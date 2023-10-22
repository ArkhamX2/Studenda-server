using CommunityToolkit.Mvvm.ComponentModel;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Client.ViewModels
{

    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string secondName;

        [ObservableProperty]
        private DateTime dateOfBirth;

        [ObservableProperty]
        private Department department;

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
        }
    }
}
