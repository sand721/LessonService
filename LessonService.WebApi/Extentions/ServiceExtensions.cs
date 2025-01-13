using System.Reflection;
using FluentValidation;
using LessonService.Application.Services;
using LessonService.Application.Services.Mapping;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using LessonService.WebApi.Exception;
using Microsoft.OpenApi.Models;

namespace LessonService.WebApi.Extentions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(builder.Configuration);
        // Adding the database context
        builder.Services.AddNpgsql<AppDbContext>("Host=localhost;Port=5432;Username=room2;Password=room2Password", options =>
        {
            options.MigrationsAssembly("LessonService.Infrastructure.EF");
        });
        
        builder.Services.AddAutoMapper(typeof(Program), typeof(LessonMapping));
        // Adding validators from the current assembly
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
        builder.Services.AddScoped<ILessonServiceApp, LessonServiceApp>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen( c=>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LessonServiceApi", Version = "v1", Description = "SnowPro LessonService API" });
        });
    }
}