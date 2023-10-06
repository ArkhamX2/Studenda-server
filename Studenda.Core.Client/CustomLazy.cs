using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

namespace Studenda.Core.Client
{
    public class CustomLazy<TView> : CommunityToolkit.Maui.Views.LazyView where TView : View, new()
    {
        public CustomLazy()
        {

        }

        public override async ValueTask LoadViewAsync()
        {
            // display a loading indicator
            Content = new ActivityIndicator { IsRunning = true }.Center();

            // simulate a long running task
            await Task.Delay(10);

            // load the view
            Content = new TView { BindingContext = BindingContext };

            SetHasLazyViewLoaded(true);
        }

    }
}
