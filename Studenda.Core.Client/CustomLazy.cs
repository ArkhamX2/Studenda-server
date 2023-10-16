using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;

namespace Studenda.Core.Client
{
    public class CustomLazy<TView> : LazyView where TView : View, new()
    {
        public override async ValueTask LoadViewAsync()
        {
            // display a loading indicator
            Content = new ActivityIndicator { IsRunning = true }.Center();

            // simulate a long running task
            await Task.Delay(10);

            // load the view
            Content = new TView { BindingContext = this.BindingContext };

            SetHasLazyViewLoaded(true);
        }

    }
}
