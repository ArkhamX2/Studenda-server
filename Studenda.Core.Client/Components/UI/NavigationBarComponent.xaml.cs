using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.Components.UI;

public partial class NavigationBarComponent : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBarComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NavigationBarComponent)bindable;

            string title = newValue as string;
        });

    NavigationBarViewModel vm = new NavigationBarViewModel();
    public NavigationBarComponent()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    public string Title
    {
        get => GetValue(TitleProperty) as string;
        set => SetValue(TitleProperty, value);
    }

    private void BurgerMenu_Clicked(object sender, EventArgs e)
    {
#if ANDROID
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Locked;
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
#endif
        Shell.Current.FlyoutIsPresented = true;
    }
}