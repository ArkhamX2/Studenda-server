using Studenda.Core.Data.Transfer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public interface ILoginRepository
    {
        Task<SecurityResponse> Login(string email, string password, string roleName);
    }
}
