using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/lessons")]
[ApiController]
public class LessonsController : ControllerBase
{
    private readonly ILessonService LessonService;

    public LessonsController(ILessonService lessonService)
    {
        LessonService = lessonService;
    }

    [HttpDelete("{lessonId}")]
    public async Task DeleteAsync(long lessonId)
    {
        await LessonService.DeleteAsync(lessonId);
    }

    [HttpPost]
    public async Task<long> CreateAsync(LessonCreateDto dto)
    {
        var lessonId = await LessonService.CreateAsync(dto);
        return lessonId;
    }
}
