using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.ViewModel
{
    public partial class ExamViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<string> examList;

        public ExamViewModel()
        {
            ExamList = new List<string>()
            {
                "Математика",
                "Алгоритмы"
            };


        }

    }
}
