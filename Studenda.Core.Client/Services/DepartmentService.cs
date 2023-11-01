using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using Studenda.Core.Data.Transfer.Security;
using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class DepartmentService : IDepartmentRepository
    {
        public async Task<List<Department>> GetAllDepartments()
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/department/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("get");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var departments = JsonConvert.DeserializeObject<List<Department>>(content);

                return await Task.FromResult(departments);
            }
            else
            {
                return null;
            }
        }
    }
}
