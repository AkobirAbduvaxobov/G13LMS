using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/teacher-courses")]
[ApiController]
public class TeacherCoursesController : ControllerBase
{
    private readonly ITeacherCourseService TeacherCourseService;

    public TeacherCoursesController(ITeacherCourseService teacherCourseService)
    {
        TeacherCourseService = teacherCourseService;
    }

    [HttpGet]
    public async Task<List<TeacherCourseGetDto>> GetAll()
    {
        return await TeacherCourseService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<TeacherCourseGetDto> GetById(long id)
    {
        return await TeacherCourseService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(TeacherCourseCreateDto dto)
    {
        return await TeacherCourseService.CreateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, TeacherCourseUpdateDto dto)
    {
        await TeacherCourseService.UpdateAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await TeacherCourseService.DeleteAsync(id);
    }
}
