using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.Components.UI;

public partial class NavigationItemComponent : ContentView
{

    public static readonly BindableProperty ItemTypeProperty = BindableProperty.Create(nameof(ItemType), typeof(string), typeof(NavigationItemComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NavigationItemComponent)bindable;
        });



    NavigationItemViewModel vm = new NavigationItemViewModel();
    public NavigationItemComponent()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    public string ItemType
    {
        get => GetValue(ItemTypeProperty) as string;
        set => SetValue(ItemTypeProperty, value);
    }


    private void NavigationItem_Tapped(object sender, TappedEventArgs e)
    {
        switch (ItemType)
        {
            case "Profile":
                vm.GoToProfile();
                break;
            case "Notifications":
                vm.GoToNotifications();
                break;
            case "Home":
                vm.GoToHome();
                break;
            case "Schedule":
                vm.GoToSchedule();
                break;
            case "Journal":
                vm.GoToJournal();
                break;
            case "Settings":
                vm.GoToSettings();
                break;
            default:
                throw new Exception("Указан неверный тип элемента навигации.");
        }
    }
}