using Studenda.Core.Model.Schedule.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface IWeekTypeRepository
    {
        Task<List<WeekType>> GetWeekTypes();
    }
}
