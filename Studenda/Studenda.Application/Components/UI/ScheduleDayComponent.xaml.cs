namespace Studenda.Components.UI;

public partial class ScheduleDayComponent : ContentView
{
    public static readonly BindableProperty WeekDayTitleProperty = BindableProperty.Create(nameof(WeekDayTitle), typeof(string), typeof(ScheduleDayComponent));


    public ScheduleDayComponent()
    {
        InitializeComponent();
    }

    public string WeekDayTitle
    {
        get => GetValue(WeekDayTitleProperty) as string;
        set => SetValue(WeekDayTitleProperty, value);
    }

}