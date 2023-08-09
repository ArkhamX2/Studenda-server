namespace Studenda.Core.Client.Components.UI;

public partial class NotificationComponent : ContentView
{
	public static readonly BindableProperty NotificationNameProperty = BindableProperty.Create(nameof(NotificationName),typeof(string), typeof(NotificationComponent), propertyChanged: (bindable, oldValue, newValue) =>
	{
		var control = (NotificationComponent)bindable;

		control.NotificationNameLabel.Text = newValue as string;
	});

    public static readonly BindableProperty NotificationDateProperty = BindableProperty.Create(nameof(NotificationDate), typeof(string), typeof(NotificationComponent), propertyChanged: (bindable, oldValue, newValue) =>
    {
        var control = (NotificationComponent)bindable;

        control.NotificationDateLabel.Text = newValue as string;
    });

    public NotificationComponent()
	{
		InitializeComponent();
	}

    public string NotificationName 
    { 
        get=>GetValue(NotificationNameProperty) as string; 
        set=>SetValue(NotificationNameProperty,value); 
    }
    public string NotificationDate
    {
        get => GetValue(NotificationDateProperty) as string;
        set => SetValue(NotificationDateProperty, value);
    }
}