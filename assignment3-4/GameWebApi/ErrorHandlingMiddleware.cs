using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }

        catch (NotFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("pelaajaa ei löytynyt");
            return;
        }
    }
}