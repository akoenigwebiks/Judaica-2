namespace Judaica_2.Auth.Middleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomAuthorizationMiddleware> _logger;

        public CustomAuthorizationMiddleware(RequestDelegate next, ILogger<CustomAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                _logger.LogInformation("Redirecting unauthorized request to /Home/Index");
                context.Response.Clear();
                context.Response.Redirect("/auth/login");
            }
        }
    }
}
