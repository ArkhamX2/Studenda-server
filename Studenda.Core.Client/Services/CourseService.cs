using Newtonsoft.Json;
using Studenda.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class CourseService : ICourseRepository
    {
        public async Task<List<Course>> GetAllCourses()
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/course/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("get");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var courses = JsonConvert.DeserializeObject<List<Course>>(content);

                return await Task.FromResult(courses);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Course>> GetCoursesByDepartment(int departmentId)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/course/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync($"get/{departmentId}");


            if (getresponse.IsSuccessStatusCode)
            {
                var jsonResponse = await getresponse.Content.ReadAsStringAsync();
                string content = getresponse.Content.ReadAsStringAsync().Result;
                var courses = JsonConvert.DeserializeObject<List<Course>>(content);

                return await Task.FromResult(courses);
            }
            else
            {
                return null;
            }
        }
    }
}
