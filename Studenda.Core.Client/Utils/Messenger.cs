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
        public DayPressedMessenger(int dayNumber) : base(dayNumber)
        {
        }
    }
    public class SubjectListCountMessenger : ValueChangedMessage<int>
    {
        public SubjectListCountMessenger(int count) : base(count)
        {
        }
    }
}
