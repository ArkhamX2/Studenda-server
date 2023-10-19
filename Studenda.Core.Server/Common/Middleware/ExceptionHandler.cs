namespace Studenda.Core.Server.Common.Middleware;

public class ExceptionHandler
{
    public ExceptionHandler(RequestDelegate requestDelegate)
    {
        RequestDelegate = requestDelegate;
    }

    private RequestDelegate RequestDelegate { get; }

    public async Task InvokeAsync(HttpContext context)
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