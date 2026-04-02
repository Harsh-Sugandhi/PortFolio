namespace API.Middleware
{

    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {

        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;


        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {

                _logger.LogError(ex, "Argument exception occurred");

                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                { message = ex.Message });

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                { message = ex.Message });

            }

        }

    }

}
