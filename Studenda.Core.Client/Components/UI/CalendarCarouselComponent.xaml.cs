namespace Studenda.Core.Client.Components.UI;

public partial class CalendarCarouselComponent : ContentView
{
    public static readonly BindableProperty WeekTypeTitleProperty = BindableProperty.Create(nameof(WeekTypeTitle), typeof(string), typeof(CalendarCarouselComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (CalendarCarouselComponent)bindable;

            //control.WeekTitleLabel.Text = (string)newValue == "Red"?"������� ������" : "����� ������";
        });


    public CalendarCarouselComponent()
	{
		InitializeComponent();
	}

    public string WeekTypeTitle 
	{ 
		get=>GetValue(WeekTypeTitleProperty) as string;
		set=>SetValue(WeekTypeTitleProperty, value); 
	}

}