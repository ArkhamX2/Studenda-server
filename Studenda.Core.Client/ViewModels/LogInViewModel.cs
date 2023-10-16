using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Views;

namespace Studenda.Core.Client.ViewModels
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
        async private void GoToGroupSelectorView()
        {
            await Shell.Current.GoToAsync($"{nameof(GroupSelectorView)}");
        }

        [RelayCommand]
        private async void Guest()
        {
            try
            {

                GoToGroupSelectorView();

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
