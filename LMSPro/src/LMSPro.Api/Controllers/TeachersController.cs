using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/teachers")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService TeacherService;

    public TeachersController(ITeacherService teacherService)
    {
        TeacherService = teacherService;
    }

    [HttpGet]
    public async Task<List<TeacherGetDto>> GetAll()
    {
        return await TeacherService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<TeacherGetDto> GetById(long id)
    {
        return await TeacherService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(TeacherCreateDto teacherCreateDto)
    {
        return await TeacherService.CreateAsync(teacherCreateDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, TeacherUpdateDto teacherUpdateDto)
    {
        await TeacherService.UpdateAsync(id, teacherUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await TeacherService.DeleteAsync(id);
    }
}
