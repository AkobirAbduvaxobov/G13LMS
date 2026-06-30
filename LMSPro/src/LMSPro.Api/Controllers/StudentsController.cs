using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService StudentService;

    public StudentsController(IStudentService studentService)
    {
        StudentService = studentService;
    }

    [HttpGet]
    public async Task<List<StudentGetDto>> GetAll()
    {
        return await StudentService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<StudentGetDto> GetById(long id)
    {
        return await StudentService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(StudentCreateDto studentCreateDto)
    {
        return await StudentService.CreateAsync(studentCreateDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, StudentUpdateDto studentUpdateDto)
    {
        await StudentService.UpdateAsync(id, studentUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await StudentService.DeleteAsync(id);
    }
}
