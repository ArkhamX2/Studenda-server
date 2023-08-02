using Studenda.ViewModel;

namespace Studenda.Components.UI;

public partial class ScheduleComponent : ContentView
{
    public static readonly BindableProperty ScheduleProperty = BindableProperty.Create(nameof(Schedule), typeof(List<DaySchedule>), typeof(ScheduleComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ScheduleComponent)bindable;

            control.ScheduleListView.ItemsSource = newValue as List<DaySchedule>;
        });

    public ScheduleComponent()
    {
        InitializeComponent();
    }

    public List<DaySchedule> Schedule
    {
        get => GetValue(ScheduleProperty) as List<DaySchedule>;
        set => SetValue(ScheduleProperty, value);
    }

}