namespace Studenda.Core.Client.Components.UI;

public partial class NavigationFooterPanel : ContentView
{
    [Obsolete]
    public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(nameof(SelectedTab), typeof(string), typeof(NavigationFooterPanel),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NavigationFooterPanel)bindable;

            string tab = newValue as string;

            Color selectedColor = Color.FromHex("#AA8DD3");

            switch (tab)
            {
                case "Home":
                    control.HomeItemIcon.Source = "home_selected.png";
                    control.HomeItemLabel.TextColor = selectedColor;
                    break;
                case "Schedule":
                    control.ScheduleItemIcon.Source = "schedule_selected.png";
                    control.ScheduleItemLabel.TextColor = selectedColor;
                    break;
                case "Journal":
                    control.JournalItemIcon.Source = "journal_selected.png";
                    control.JournalItemLabel.TextColor = selectedColor;
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