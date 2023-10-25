using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Services;
using Studenda.Core.Client.Views;
using Studenda.Core.Data.Transfer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModels
{
    public partial class VerificationApproveViewModel
    {
        readonly ILoginRepository loginRepository = new LoginService();

        [RelayCommand]
        async private void GoToHomeView()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
        }

        [RelayCommand]
        private void GetCode()
        {
            try
            {
                //Логика повторного получения кода
                GoToHomeView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [RelayCommand]
        private async Task ApproveCodeAsync()
        {
            try
            {
                SecurityResponse loginResponse = await loginRepository.Login("test2@test.ru","Test-22222","admin");

                if (loginResponse != null)
                {
                    GoToHomeView();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        $"You entered {123} and {123}. Server Error",
                        "OK");
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
