using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.Views;

namespace Studenda.Core.Client.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        public AppShellViewModel()
        {
        }

        [RelayCommand]
        async private void LogOut()
        {
            await Shell.Current.GoToAsync($"//{nameof(LogInView)}");
        }
    }
}
