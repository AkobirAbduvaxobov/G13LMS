using LMSPro.Api.Repositories;
using LMSPro.Api.Services;

namespace LMSPro.Api.Configurations;

public static class DependicyInjectionConfigurations
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<ILessonService, LessonService>();
        builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
        builder.Services.AddScoped<ITeacherService, TeacherService>();
        builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
        builder.Services.AddScoped<IExamService, ExamService>();
        builder.Services.AddScoped<IHomeworkService, HomeworkService>();
        builder.Services.AddScoped<IResourceService, ResourceService>();
        builder.Services.AddScoped<IStudentService, StudentService>();
    }
}
