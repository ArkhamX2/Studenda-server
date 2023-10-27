using CommunityToolkit.Mvvm.Messaging;
using Studenda.Core.Client.Utils;
using Studenda.Core.Client.ViewModels;

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
        WeakReferenceMessenger.Default.Register<ScheduleComponent, DayPressedMessenger>(
this,
async (recipient, message) =>
{
    await recipient.Dispatcher.DispatchAsync(
        async () =>
        {
            int scroll = 0;
            if (message.Value < 6)
            {
                for (int i = 0; i < message.Value; i++)
                {
                    try
                    {
                        scroll += Schedule[i].SubjectList.Count * 74 + 70;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            WeakReferenceMessenger.Default.Send(new SubjectListCountMessenger(scroll)); ;
        });
});
    }

    public List<DaySchedule> Schedule
    {
        get => GetValue(ScheduleProperty) as List<DaySchedule>;
        set => SetValue(ScheduleProperty, value);
    } 

}