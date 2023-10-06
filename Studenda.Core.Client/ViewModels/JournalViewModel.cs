using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModel
{
    public partial class JournalViewModel:ObservableObject
    {
        [ObservableProperty]
        private string journal;

        public JournalViewModel() { }
    }
}
