using LMSPro.Api.Dtos;
using LMSPro.Api.Filters;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

//[TypeFilter(typeof(LoggingActionFilter))]
[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService CourseService;

    public CoursesController(ICourseService courseService)
    {
        CourseService = courseService;
    }

    [HttpPost]
    public async Task<long> CreateCourse(CourseCreateDto courseCreateDto)
    {
        var courseId = await CourseService.CreateAsync(courseCreateDto);
        return courseId;
    }

    [HttpGet]
    public async Task<IEnumerable<CourseGetDto>> GetAllCourses()
    {
        var courses = await CourseService.GetAllAsync();
        return courses;
    }

    [HttpGet("{id}")]
    public async Task<CourseGetDto> GetById(long id)
    {
        var course = await CourseService.GetByIdAsync(id);
        return course;
    }

    [HttpDelete("{id}")]
    public async Task DeleteCourse(long id)
    {
        await CourseService.DeleteAsync(id);
    }

    [HttpPut("{id}")]
    public async Task UpdateCourse(long id, CourseUpdateDto courseUpdateDto)
    {
        await CourseService.UpdateAsync(id, courseUpdateDto);
    }
}
