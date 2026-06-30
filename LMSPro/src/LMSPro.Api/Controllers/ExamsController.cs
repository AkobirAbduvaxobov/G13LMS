using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/exams")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IExamService ExamService;

    public ExamsController(IExamService examService)
    {
        ExamService = examService;
    }

    [HttpGet]
    public async Task<List<ExamGetDto>> GetAll()
    {
        return await ExamService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ExamGetDto> GetById(long id)
    {
        return await ExamService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(ExamCreateDto examCreateDto)
    {
        return await ExamService.CreateAsync(examCreateDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, ExamUpdateDto examUpdateDto)
    {
        await ExamService.UpdateAsync(id, examUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await ExamService.DeleteAsync(id);
    }
}
