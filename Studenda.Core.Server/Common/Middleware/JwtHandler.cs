using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using static Studenda.Core.Server.Common.Middleware.HttpStatus;
using Studenda.Core.Server.Security.Service;

namespace Studenda.Core.Server.Common.Middleware
{
    public class JwtHandler
    {
        private RequestDelegate RequestDelegate { get; }
        private TokenValidationParameters validationParameters;

        public JwtHandler(RequestDelegate requestDelegate)
        {
            RequestDelegate = requestDelegate;

            validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtManager.Issuer,
                ValidAudience = JwtManager.Audience,
                ClockSkew = TimeSpan.FromMinutes(2),
                IssuerSigningKey = JwtManager.GetSymmetricSecurityKey()
            };
        }

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

                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(Token, validationParameters, out validatedToken);
                //еще не работает
                if (principal.HasClaim("Role","asd"))
                {
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
    }
}
