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

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LessonCreateDto lessonCreateDto)
    {
        var lessonId = await LessonService.CreateAsync(lessonCreateDto);
        return Ok(lessonId);
    }

    [HttpDelete("{lessonId}")]
    public async Task DeleteAsync(long lessonId)
    {
        await LessonService.DeleteAsync(lessonId);
    }

    [HttpPut("{lessonId}")]
    public async Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        await LessonService.UpdateAsync(lessonId, lessonUpdateDto);
    }
}