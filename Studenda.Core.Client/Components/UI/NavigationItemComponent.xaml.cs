using Studenda.Core.Client.ViewModels;

namespace Studenda.Core.Client.Components.UI;

public partial class NavigationItemComponent : ContentView
{

    public static readonly BindableProperty ItemTypeProperty = BindableProperty.Create(nameof(ItemType), typeof(string), typeof(NavigationItemComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NavigationItemComponent)bindable;
        });

    public NavigationItemComponent()
    {
        InitializeComponent();
    }

    public string ItemType
    {
        get => GetValue(ItemTypeProperty) as string;
        set => SetValue(ItemTypeProperty, value);
    }


    private void NavigationItem_Tapped(object sender, TappedEventArgs e)
    {
        var _bindingContext = this.BindingContext as HomeViewModel;
        var _subContext = new NavigationItemViewModel();

        switch (ItemType)
        {
            case "Profile":
                _bindingContext.NavigationItemViewModel.GoToProfile(_bindingContext);
                break;
            case "Notifications":
                _bindingContext.NavigationItemViewModel.GoToNotifications(_bindingContext);
                break;
            case "Home":
                _bindingContext.NavigationItemViewModel.GoToHome(_bindingContext);
                break;
            case "Schedule":
                _bindingContext.NavigationItemViewModel.GoToSchedule(_bindingContext);
                break;
            case "Journal":
                _bindingContext.NavigationItemViewModel.GoToJournal(_bindingContext);
                break;
            case "Login":
                _subContext.GoToLogin();
                break;
            case "Verification":
                _subContext.GoToVerification();
                break;
            case "Label":
                break;
            default:
                throw new Exception("������ �������� ��� �������� ���������.");
        }
    }
}