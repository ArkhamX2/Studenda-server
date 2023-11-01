using Newtonsoft.Json;
using Studenda.Core.Data.Transfer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Studenda.Core.Client.Services
{
    public class LoginService : ILoginRepository
    {

        private class UserInfo
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public string RoleName { get;set; }

            public UserInfo(string email, string password, string roleName)
            {
                this.Email = email;
                this.Password = password;
                this.RoleName = roleName;
            }
        }
        public async Task<SecurityResponse> Login(string email, string password, string roleName)
        {
            var client = new HttpClient();

            string url = "http://88.210.3.137/api/security/";
            client.BaseAddress = new Uri(url);

            using HttpResponseMessage getresponse = await client.GetAsync("test");

            using HttpResponseMessage registerResponse = await client.PostAsJsonAsync(
                "register",
                new RegisterRequest
                {
                    Email = email,
                    Password = password,
                    RoleName = roleName,
                });

            using HttpResponseMessage loginResponse = await client.PostAsJsonAsync(
                "login",
                new LoginRequest
                {
                    Email = email,
                    Password = password,
                    RoleName = roleName,
                });

            if (loginResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await loginResponse.Content.ReadAsStringAsync();
                string content = loginResponse.Content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<SecurityResponse>(content);

                return await Task.FromResult(user);
            }
            else
            {
                return null;
            }
        }
    }
}
