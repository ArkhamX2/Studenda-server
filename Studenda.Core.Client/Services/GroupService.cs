using Newtonsoft.Json;
using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class GroupService : IGroupRepository
    {
        public async Task<List<Group>> GetAllGroups()
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/group/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("get");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var groups = JsonConvert.DeserializeObject<List<Group>>(content);

                return await Task.FromResult(groups);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Group>> GetGroupsByCourse(int courseId)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/group/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync($"get/{courseId}");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var groups = JsonConvert.DeserializeObject<List<Group>>(content);

                return await Task.FromResult(groups);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Group>> GetGroupsByDepartment(int departmentId)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/group/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync($"get/{departmentId}");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var groups = JsonConvert.DeserializeObject<List<Group>>(content);

                return await Task.FromResult(groups);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Group>> GetGroupsByDepartmentAndCourse(int departmentId, int courseId)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/group/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync($"get/{departmentId}/{courseId}");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var groups = JsonConvert.DeserializeObject<List<Group>>(content);

                return await Task.FromResult(groups);
            }
            else
            {
                return null;
            }
        }
    }
}
