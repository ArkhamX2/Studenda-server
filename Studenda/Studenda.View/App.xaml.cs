using Studenda.Components.UI;

namespace Studenda;

public partial class App : Application
{
    bool _launched = false;
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(StudendaEntry), (Handler, view) =>
        {
#if __ANDROID__
            Handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
    }
    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);
        window.Activated += Window_Activated;
        return window;
    }

    private async void Window_Activated(object sender, EventArgs e)
    {

#if __ANDROID__

#endif

#if WINDOWS
        if (!_launched)
        {
            const int DefaultWidth = 1368;
            const int DefaultHeight = 760;

            const int MinimumWidth = 440;
            const int MinimumHeight = 720;

            var window = sender as Window;

            // change window size.
            window.Width = DefaultWidth;
            window.Height = DefaultHeight;

            window.MinimumWidth = MinimumWidth;
            window.MinimumHeight = MinimumHeight;
            // give it some time to complete window resizing task.
            await window.Dispatcher.DispatchAsync(() => { });

            var disp = DeviceDisplay.Current.MainDisplayInfo;

            // move to screen center
            window.X = (disp.Width / disp.Density - window.Width) / 2;
            window.Y = (disp.Height / disp.Density - window.Height) / 2;

            _launched = true;
        }
#endif
    }
}
