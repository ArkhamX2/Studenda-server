using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModels
{
    public partial class VerificationApproveViewModel
    {
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
        private void ApproveCode()
        {
            try
            {
                //Логика по авторизации
                GoToHomeView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
