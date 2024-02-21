using Microsoft.IdentityModel.Tokens;
using Studenda.Core.Server.Security.Service;

namespace Studenda.Core.Server.Common.Middleware
{
    public class ExceptionHandler
    {
        private RequestDelegate RequestDelegate { get; }
        public ExceptionHandler(RequestDelegate requestDelegate)
        {
            RequestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await RequestDelegate.Invoke(context);
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
