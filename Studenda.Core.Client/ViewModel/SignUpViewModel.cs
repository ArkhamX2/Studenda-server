using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Studenda.Core.Client.ViewModel
{
    public partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string studentID;

        public SignUpViewModel()
        {
        }

        [RelayCommand]
        private async void CreateAccount()
        {
            try
            {
                //TODO: Создание аккаунта
                await Application.Current.MainPage.DisplayAlert(
                    "Submit",
                    $"You entered {Username} and {Password}",
                    "OK");
            }
            catch (Exception e)
            {
                //TODO: Обработка ошибок регистрации
                throw new Exception(e.Message);
            }
        }

    }
}
