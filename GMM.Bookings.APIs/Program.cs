using FluentValidation;
using GMM.Bookings.APIs.Middlewares;
using GMM.Bookings.APIs.Validators;
using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Models.Exceptions;
using GMM.Bookings.Services;
using GMM.Bookings.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDb>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(AppDb)));
});

builder.Services.AddScoped<App>();
builder.Services.AddScoped<IValidator<NewCourse>, NewCourseValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  //options.SuppressConsumesConstraintForFormFileParameters = true;
  //options.SuppressInferBindingSourcesForParameters = true;
  options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey
      (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateIssuerSigningKey = true,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
  };
});
//.AddCookie(x =>
// {
//   x.AccessDeniedPath = "/mysignin";
//   x.LoginPath = "";
//   x.LogoutPath = "";
// });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
  appError.Run(async context =>
  {
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "application/json";
    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
    if (contextFeature != null)
    {
      app.Logger.LogError($"Something went wrong: {contextFeature.Error}");

      if (contextFeature.Error.GetType().IsAssignableTo(typeof(AppException)))
      {
        var myApp = app.Services.GetRequiredService<App>();

        var ex = (AppException)contextFeature.Error;
        context.Response.StatusCode = ex.HttpStatusCode;
        await context.Response.WriteAsync(new ErrorDetails()
        {
          StatusCode = ex.HttpStatusCode,
          Message = ex.Message,
          CurrentUserId = myApp.CurrentUser?.Id,
          CurrentUserName = myApp.CurrentUser?.Name 
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
  });
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuth();

//app.UseMiddleware<ExceptionTransformMiddleware>();
//app.UseExceptionTransform();

app.MapControllers();
app.Run();
