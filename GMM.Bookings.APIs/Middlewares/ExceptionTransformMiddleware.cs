using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GMM.Bookings.APIs.Middlewares
{
  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class ExceptionTransformMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionTransformMiddleware> logger;

    public ExceptionTransformMiddleware(RequestDelegate next, ILogger<ExceptionTransformMiddleware> logger)
    {
      _next = next;
      this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      await _next.Invoke(context);

      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";

      var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
      if (contextFeature != null)
      {
        logger.LogError($"Something went wrong: {contextFeature.Error}");

        if (contextFeature.Error.GetType().IsAssignableTo(typeof(AppException)))
        {
          var ex = (AppException)contextFeature.Error;
          context.Response.StatusCode = ex.HttpStatusCode;
          await context.Response.WriteAsync(new ErrorDetails()
          {
            StatusCode = ex.HttpStatusCode,
            Message = ex.Message
          }.ToString());
        }
        else
        {
          await context.Response.WriteAsync(new ErrorDetails()
          {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error."
          }.ToString());
        }
      }
    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class ExceptionTransformMiddlewareExtensions
  {
    public static IApplicationBuilder UseExceptionTransform(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ExceptionTransformMiddleware>();
    }
  }
} 
