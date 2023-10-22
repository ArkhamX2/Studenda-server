using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Utils
{
    public class DayPressedMessenger : ValueChangedMessage<int>
    {
        public DayPressedMessenger(int count) : base(count)
        {
        }
    }
    public class SubjectListCountMessenger : ValueChangedMessage<int>
    {
        public SubjectListCountMessenger(int count) : base(count)
        {
        }
    }
    public class ReloadScheduleMessenger : ValueChangedMessage<int>
    {
        public ReloadScheduleMessenger(int count) : base(count)
        {
        }
    }
}
