using CommunityToolkit.Mvvm.Messaging;
using Studenda.Core.Client.Utils;

namespace Studenda.Core.Client.Components.UI.Wrappers;

public partial class ScheduleComponentWrapper : ContentView
{
    public ScheduleComponentWrapper()
	{        
        WeakReferenceMessenger.Default.Register<ScheduleComponentWrapper, SubjectListCountMessenger>(
        this,
        async (recipient, message) =>
        {
            await recipient.Dispatcher.DispatchAsync(
                async () =>
                {
                    await recipient.ScheduleScrollView.ScrollToAsync(0, message.Value, true);
                });
        });
        InitializeComponent();
    }
}