using Studenda.Core.Data.Transfer.Security;
using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetGroupsByDepartment(int departmentId);
        Task<List<Group>> GetGroupsByCourse(int courseId);
        Task<List<Group>> GetGroupsByDepartmentAndCourse(int departmentId,int courseId);
        Task<List<Group>> GetAllGroups();
    }

}
