﻿using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModels
{
    public partial class VerificationViewModel
    {
        [RelayCommand]
        async private void GoToVerificationApproveView()
        {
            await Shell.Current.GoToAsync($"//{nameof(VerificationApproveView)}");
        }

        [RelayCommand]
        private void SignIn()
        {
            try
            {
                GoToVerificationApproveView();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
