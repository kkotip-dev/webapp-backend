using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace KkotipAppApi.Filters;

public class AuthenticationFilter(ApplicationContext context, ILogger<AuthenticationFilter> logger) : IAsyncActionFilter
{
    private readonly ILogger<AuthenticationFilter> _logger = logger;
    private readonly ApplicationContext _context = context;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Path.Value == "/api/login")
        {
            _logger.LogTrace("Passing login request through");
            await next();
            return;
        }

        var authorizationParts = context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(' ');
        if (authorizationParts == null)
        {
            _logger.LogDebug("Authorization header not found");
            context.Result = new UnauthorizedObjectResult(new { message = "No Authorization header" });
            return;
        }

        if (authorizationParts.Length != 2 || authorizationParts[0] != "Bearer")
        {
            _logger.LogDebug("Malformed Authorization header");
            context.Result = new UnauthorizedObjectResult(new { message = "Malformed Authorization header" });
            return;
        }

        var token = authorizationParts[1];
        var userToken = _context.Tokens.Include(x => x.User).Where(x => x.Token == token).FirstOrDefault();

        if (userToken == null)
        {
            _logger.LogDebug("User not found");
            context.Result = new UnauthorizedObjectResult(new { message = "Unauthorized" });
            return;
        }

        context.HttpContext.Items.Add("User", userToken.User);

        await next();
    }
}
