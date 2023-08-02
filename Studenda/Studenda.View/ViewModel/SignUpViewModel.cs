using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Studenda.ViewModel
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
        private void CreateAccount()
        {
            try
            {
                //TODO: Создание аккаунта
            }
            catch (Exception e)
            {
                //TODO: Обработка ошибок регистрации
                throw new Exception(e.Message);
            }
        }

    }
}
