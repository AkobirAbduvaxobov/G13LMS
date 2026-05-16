using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

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

}
