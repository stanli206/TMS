using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.web.Middleware
{
    public class ProblemDetailsMiddleware
    {
        private readonly RequestDelegate _next;

        public ProblemDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (DomainException ex)
            {
                ctx.Response.StatusCode = 400;
                await ctx.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Title = "Domain error",
                    Detail = ex.Message,
                    Status = 400
                });
            }
        }
    }
}
