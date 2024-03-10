using System.Net;

namespace Studenda.Server.Middleware;

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
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorType = exception.GetType().ToString(),
                    ErrorMessage = exception.Message
                });
            }
        }
    }

