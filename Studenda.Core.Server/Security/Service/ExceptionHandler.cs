namespace Studenda.Core.Server.Security.Service
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                string errorType = ex.GetType().ToString();
                string errorMessage = ex.Message;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { ErrorType = errorType, ErrorMessage = errorMessage });
            }
        }
    }
}
