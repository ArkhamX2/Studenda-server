using CommunityToolkit.Mvvm.Messaging;
using Studenda.Core.Client.Utils;

namespace Studenda.Core.Client.Components.UI;

public partial class CalendarCarouselComponent : ContentView
{
    public static readonly BindableProperty WeekTypeTitleProperty = BindableProperty.Create(nameof(WeekTypeTitle), typeof(string), typeof(CalendarCarouselComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CalendarCarouselComponent)bindable;

            //control.WeekTitleLabel.Text = (string)newValue == "Red"?"������� ������" : "����� ������";
        });
    static DateTime datenow = DateTime.Now;
    List<DayOfWeek> weekDays = new List<DayOfWeek>() {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday };

    public CalendarCarouselComponent()
    {
        InitializeComponent();
        CalculateDates();
    }

    public string WeekTypeTitle
    {
        get => GetValue(WeekTypeTitleProperty) as string;
        set => SetValue(WeekTypeTitleProperty, value);
    }
    
    private void CalculateDates()
    {
        List<Button> date = new List<Button>() {
        FirstDate,
        SecondDate,
        ThirdDate,
        FourthDate,
        FifthDate,
        SixthDate
        };
        WeekDate(date);
    }
    private void WeekDate(List<Button> date)
    {
        for (int currentDayIndex = 0; currentDayIndex < weekDays.Count; currentDayIndex++)
        {
            if (weekDays[currentDayIndex] == datenow.DayOfWeek)
            {
                datenow=datenow.AddDays(currentDayIndex * -1);
            }
        }
        for (int i=0; i<date.Count; i++)
        {
            date[i].Text = datenow.AddDays(i).ToString("dd");
        }
    }

    private void FirstDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(0));
    }

    private void SecondDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(1));
    }

    private void ThirdDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(2));
    }

    private void FourthDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(3));
    }

    private void FifthDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(4));
    }

    private void SixthDate_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new DayPressedMessenger(5));
    }

    private void LeftArrow_Clicked(object sender, EventArgs e)
    {
        datenow = datenow.AddDays(-7);
        CalculateDates();
    }

    private void RightArrow_Clicked(object sender, EventArgs e)
    {
        datenow = datenow.AddDays(7);
        CalculateDates();
    }
}