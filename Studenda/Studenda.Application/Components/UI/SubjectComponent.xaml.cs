namespace Studenda.Components.UI;

public partial class SubjectComponent : ContentView
{
    public static readonly BindableProperty SubjectTimeStartProperty = BindableProperty.Create(nameof(SubjectTimeStart), typeof(string), typeof(SubjectComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SubjectComponent)bindable;

            control.SubjectTimeStartLabel.Text = newValue as string;
        });

    public static readonly BindableProperty SubjectTimeEndProperty = BindableProperty.Create(nameof(SubjectTimeEnd), typeof(string), typeof(SubjectComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SubjectComponent)bindable;

            control.SubjectTimeEndLabel.Text = newValue as string;
        });

    public static readonly BindableProperty SubjectTitleProperty = BindableProperty.Create(nameof(SubjectTitle), typeof(string), typeof(SubjectComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SubjectComponent)bindable;

            control.SubjectTitleLabel.Text = newValue as string;
        });

    public static readonly BindableProperty SubjectPlaceProperty = BindableProperty.Create(nameof(SubjectPlace), typeof(string), typeof(SubjectComponent),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (SubjectComponent)bindable;

            control.SubjectPlaceLabel.Text = newValue as string;
        });


    public SubjectComponent()
    {
        InitializeComponent();
    }

    public string SubjectTimeStart
    {
        get => GetValue(SubjectTimeStartProperty) as string;
        set => SetValue(SubjectTimeStartProperty, value);
    }

    public string SubjectTimeEnd
    {
        get => GetValue(SubjectTimeEndProperty) as string;
        set => SetValue(SubjectTimeEndProperty, value);
    }

    public string SubjectTitle
    {
        get => GetValue(SubjectTitleProperty) as string;
        set => SetValue(SubjectTitleProperty, value);
    }

    public string SubjectPlace
    {
        get => GetValue(SubjectPlaceProperty) as string;
        set => SetValue(SubjectPlaceProperty, value);
    }
}