using FluentValidation;
using LMSPro.Api.Filters;
using LMSPro.Api.Repositories;
using LMSPro.Api.Services;
using LMSPro.Api.Dtos;

namespace LMSPro.Api.Configurations; 

public static class DependicyInjectionConfigurations
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<ILessonService, LessonService>();
        builder.Services.AddScoped<LoggingActionFilter>();
        builder.Services.AddScoped<CustomExceptionFilter>();
        //builder.Services.AddScoped<IBaseRepository, BaseRepository>();
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddValidatorsFromAssemblyContaining<QuestionCreateDtoValidator>();
    }
}