using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/homeworks")]
[ApiController]
public class HomeworksController : ControllerBase
{
    private readonly IHomeworkService HomeworkService;

    public HomeworksController(IHomeworkService homeworkService)
    {
        HomeworkService = homeworkService;
    }

    [HttpGet]
    public async Task<List<HomeworkGetDto>> GetAll()
    {
        return await HomeworkService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<HomeworkGetDto> GetById(long id)
    {
        return await HomeworkService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(HomeworkCreateDto homeworkCreateDto)
    {
        return await HomeworkService.CreateAsync(homeworkCreateDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, HomeworkUpdateDto homeworkUpdateDto)
    {
        await HomeworkService.UpdateAsync(id, homeworkUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await HomeworkService.DeleteAsync(id);
    }
}
