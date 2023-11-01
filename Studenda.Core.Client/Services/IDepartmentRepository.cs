using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartments();
    }
}
