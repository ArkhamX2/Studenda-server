using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Studenda.View.AndroidView;

namespace Studenda.ViewModel
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
