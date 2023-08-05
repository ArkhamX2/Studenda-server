﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.View.AndroidView;

namespace Studenda.ViewModel
{
    public partial class NavigationBarViewModel : ObservableObject
    {
        [RelayCommand]
        async private void GoToNotificationView()
        {
            await Shell.Current.GoToAsync($"///{nameof(NotificationView)}");
        }

        [RelayCommand]
        private void Notifications() => GoToNotificationView();

        public NavigationBarViewModel()
        {
        }
    }
}