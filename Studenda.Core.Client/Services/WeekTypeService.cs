using Newtonsoft.Json;
using Studenda.Core.Model.Schedule.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class WeekTypeService : IWeekTypeRepository
    {
        public async Task<List<WeekType>> GetWeekTypes()
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/weektype/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("get");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var weekTypes = JsonConvert.DeserializeObject<List<WeekType>>(content);

                return await Task.FromResult(weekTypes);
            }
            else
            {
                return null;
            }
        }
    }
}
