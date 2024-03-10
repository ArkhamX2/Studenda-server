using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using static Studenda.Core.Server.Common.Middleware.HttpStatus;
using Studenda.Core.Server.Security.Service;
using Studenda.Server.Configuration.Repository;
using ConfigurationManager = Studenda.Server.Configuration.ConfigurationManager;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Studenda.Server.Service.Security;

namespace Studenda.Core.Server.Common.Middleware
{
    public class JwtHandler(ConfigurationManager configuration, RequestDelegate requestDelegate, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,TokenService tokenService)
    {
        private RequestDelegate RequestDelegate { get; } = requestDelegate;
        private TokenConfiguration Configuration { get; } = configuration.TokenConfiguration;
        private UserManager<IdentityUser> userManager { get; }=userManager;
        private RoleManager<IdentityRole> roleManager { get;}=roleManager;
        private TokenService tokenService { get;}=tokenService;
       
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var Token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(Token))
                {
                    context.Response.StatusCode = (int)UnAuthorized;
                    await context.Response.WriteAsync("Missing or invalid token");
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = configuration.TokenConfiguration.GetValidationParameters();
                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(Token, validationParameters, out validatedToken);
                var jwttoken=(JwtSecurityToken)validatedToken;                       
                if (await CheckUser(jwttoken))
                {
                   var user = await userManager.FindByIdAsync(jwttoken.Claims.First(x => x.Type=="ClaimLabelUserEmail").Value);
                   var role=roleManager.Roles.Where(role=>role.Name==jwttoken.Claims.First(x => x.Type=="ClaimLabelUserRole").Value).ToList();
                   var token = tokenService.CreateNewToken(user, role);
                   context.Response.Headers.Add("token",token);
                   await RequestDelegate.Invoke(context);
                }
            }
            catch (Exception exception)
            {
                // TODO: логгирование
                // TODO: вынести коды ответов в константы
                context.Response.StatusCode = (int)ServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorType = exception.GetType().ToString(),
                    ErrorMessage = exception.Message
                });
            }
        }
        private async Task<bool> CheckUser(JwtSecurityToken jwttoken)
        {
            var UserName = jwttoken.Claims.First(x => x.Type=="ClaimLabelUserName").Value;
            var Email = jwttoken.Claims.First(x => x.Type=="ClaimLabelUserEmail").Value;
            var Id = jwttoken.Claims.First(x => x.Type=="ClaimLabelUserEmail").Value;
            var Role = jwttoken.Claims.First(x => x.Type=="ClaimLabelUserRole").Value;
            var user =await userManager.FindByIdAsync(Id);
            if(user.Email==Email && user.UserName==UserName && await userManager.IsInRoleAsync(user,Role))
            {
                return true;
            }
            return false;
        }

    }
}
