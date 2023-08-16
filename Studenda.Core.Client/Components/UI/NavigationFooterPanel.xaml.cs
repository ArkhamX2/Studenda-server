namespace Studenda.Core.Client.Components.UI;

public partial class NavigationFooterPanel : ContentView
{
    public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(nameof(SelectedTab), typeof(string), typeof(NavigationFooterPanel),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NavigationFooterPanel)bindable;

            string tab = newValue as string;

            var selectedColor = (Color)control.FooterGrid.BackgroundColor.AddLuminosity(0.05f);

            switch (tab)
            {
                case "Home":
                    control.HomeItem.BackgroundColor = selectedColor;
                    break;
                case "Schedule":
                    control.ScheduleItem.BackgroundColor = selectedColor;
                    break;
                case "Journal":
                    control.JournalItem.BackgroundColor = selectedColor;
                    break;
                default:
                    throw new Exception("Указано недопустимое значение");
            }
        }); 

    public NavigationFooterPanel()
    {
        InitializeComponent();
    }

    public string SelectedTab 
    { 
        get => GetValue(SelectedTabProperty) as string; 
        set=> SetValue(SelectedTabProperty,value); 
    }
}