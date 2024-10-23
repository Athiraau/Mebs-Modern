
//28-06-2024  
//added modules

using Business.Contracts;
using Business.Services;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Repository;
using DataAccess.Entities;
using FluentValidation;
using DataAccess.Dto.Request;
using MebsAppModuleAPI.Validators;
using Business.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Text;
using Newtonsoft;
using Microsoft.Extensions.Logging;
using MebsAppModuleAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);

builder.Logging.AddNLog(logPath).SetMinimumLevel(LogLevel.Trace);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DataContext>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddTransient<IServiceWrapper, ServiceWrapper>();
builder.Services.AddTransient<HelperWrapper>();
builder.Services.AddTransient<ILoansModuleService,LoansModuleService>();
builder.Services.AddTransient<IHRMModuleService,HRMModuleService>();
builder.Services.AddTransient<IOthersModuleService, OthersModuleService>();

builder.Services.AddTransient<LoansModuleRepo>();
builder.Services.AddTransient<HRMModuleRepo>();
builder.Services.AddTransient<OthersModuleRepo>();
builder.Services.AddTransient<DtoWrapper>();
builder.Services.AddTransient<ErrorResponse>();
builder.Services.AddTransient<IJwtUtils,JwtUtils>();
builder.Services.AddTransient<IValidator<PostReqDto>, CommonValidator>();
builder.Services.AddTransient<CommonValidationHelper>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true);
}));


builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage();

app.UseSwagger();
// This middleware serves the Swagger documentation UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mebs App Module API V1");
});
//}




//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<JwtMiddlewareExtension>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
