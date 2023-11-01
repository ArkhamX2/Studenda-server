using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCoursesByDepartment(int departmentId);
        Task<List<Course>> GetAllCourses();
    }
}
