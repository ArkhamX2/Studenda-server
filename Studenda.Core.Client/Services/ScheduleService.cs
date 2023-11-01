using Newtonsoft.Json;
using Studenda.Core.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class ScheduleService : IScheduleRepository
    {
        public async Task<List<Subject>> GetSubjects(int groupId, int weekTypeId)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/subjects/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("/student/get/{groupId}/{weekTypeId}");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var subjects = JsonConvert.DeserializeObject<List<Subject>>(content);

                return await Task.FromResult(subjects);
            }
            else
            {
                return null;
            }
        }
    }
}
