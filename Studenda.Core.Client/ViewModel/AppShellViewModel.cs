using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.Core.Client.View.AndroidView;

namespace Studenda.Core.Client.ViewModel
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
