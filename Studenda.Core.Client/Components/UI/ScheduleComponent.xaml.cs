using Studenda.Core.Client.ViewModel;

namespace Studenda.Core.Client.Components.UI;

public partial class ScheduleComponent : ContentView
{
    public static readonly BindableProperty ScheduleProperty = BindableProperty.Create(nameof(Schedule), typeof(List<DaySchedule>), typeof(ScheduleComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ScheduleComponent)bindable;

            List<DaySchedule> scheduleList = newValue as List<DaySchedule>;
            BindableLayout.SetItemsSource(control.ScheduleListView, scheduleList);
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