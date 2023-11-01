using Studenda.Core.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface IScheduleRepository
    {
        Task<List<Subject>> GetSubjects(int groupId, int weekTypeId);
    }
}
