using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.View.AndroidView;

namespace Studenda.Core.Client.ViewModel
{
    public partial class LogInViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool activityIndicatorIsRunning = true;

        public LogInViewModel()
        {
        }

        [RelayCommand]
        async private void GoToHomeView()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
        }

        [RelayCommand]
        async private void GoToVerificationView()
        {
            await Shell.Current.GoToAsync($"{nameof(VerificationView)}");
        }

        [RelayCommand]
        private async void Guest()
        {
            try
            {

                GoToHomeView();

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
        private void Verification()
        {
            try
            {
                GoToVerificationView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}
