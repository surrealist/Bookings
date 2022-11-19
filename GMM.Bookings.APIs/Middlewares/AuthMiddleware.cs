using GMM.Bookings.Models;
using GMM.Bookings.Services;
using Microsoft.AspNetCore.Mvc;

namespace GMM.Bookings.APIs.Middlewares
{
  public class AuthMiddleware
  {
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, [FromServices] App app)
    {
      
      if (httpContext != null && httpContext.User != null && httpContext.User.Identity != null
        && httpContext.User.Identity.IsAuthenticated)
      {
        // TODO: consider find actually GUID of the current user. 
        var id = httpContext.User.Claims.SingleOrDefault(x => x.Type == "gmm-id");
        
        if (id != null)
        {
          var role = httpContext.User.Claims.SingleOrDefault(x => x.Type == "gmm-role");
          var userName = httpContext.User.Identity!.Name;

          app.SetCurrentUser(new Guid(id.Value), userName!, role!.Value);
        }
      }

      await _next(httpContext!);
    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class AuthMiddlewareExtensions
  {
    public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<AuthMiddleware>();
    }
  }
}
