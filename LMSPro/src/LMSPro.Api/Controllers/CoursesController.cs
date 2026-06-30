using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace LMSPro.Api.Controllers;

[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService CourseService;
    private readonly IOutputCacheStore OutputCacheStore;

    public CoursesController(ICourseService courseService, IOutputCacheStore outputCacheStore)
    {
        CourseService = courseService;
        OutputCacheStore = outputCacheStore;
    }

    [HttpPost]
    public async Task<long> CreateCourse(CourseCreateDto courseCreateDto)
    {
        var courseId = await CourseService.CreateAsync(courseCreateDto);
        return courseId;
    }

    [HttpGet]
    [OutputCache(PolicyName = "ProductsCache")]
    public async Task<IEnumerable<CourseGetDto>> GetAllCourses()
    {
        var courses = await CourseService.GetAllAsync();
        return courses;
    }

    [HttpGet("{id}")]
    [OutputCache(Duration = 30)]
    public async Task<CourseGetDto> GetById(long id)
    {
        var course = await CourseService.GetByIdAsync(id);
        return course;
    }

    [HttpDelete("{id}")]
    public async Task DeleteCourse(long id)
    {
        await CourseService.DeleteAsync(id);
        await OutputCacheStore.EvictByTagAsync("products", default);
    }

    [HttpPut("{id}")]
    public async Task UpdateCourse(long id, CourseUpdateDto courseUpdateDto)
    {
        await CourseService.UpdateAsync(id, courseUpdateDto);
    }
}
