using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.View.AndroidView;

namespace Studenda.ViewModel
{
    public partial class LogInViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool activityIndicatorIsRunning = true;

        [ObservableProperty]
        private string username = "";

        [ObservableProperty]
        private string password;

        public LogInViewModel()
        {
        }

        [RelayCommand]
        async private void GoToScheduleView()
        {
            await Shell.Current.GoToAsync($"//{nameof(ScheduleView)}");
        }

        [RelayCommand]
        async private void GoToRegisterView()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpView)}");
        }

        [RelayCommand]
        private async void Submit()
        {
            try
            {
                //TODO: Реализация входа в аккаунт

                GoToScheduleView();

                //Alert user about something
                await Application.Current.MainPage.DisplayAlert(
                    "Submit",
                    $"You entered {Username} and {Password}",
                    "OK");
            }
            catch (Exception e)
            {
                //TODO: Обработка ошибок входа
                throw new Exception(e.Message);
            }
            finally
            {

            }
        }

        [RelayCommand]
        private void Create()
        {
            try
            {
                GoToRegisterView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
