using LMSPro.Api.Dtos;
using LMSPro.Api.Filters;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace LMSPro.Api.Controllers;


[Route("api/questions")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService QuestionService;
    private readonly IOutputCacheStore OutputCacheStore;
    public QuestionsController(IQuestionService questionService, IOutputCacheStore outputCacheStore)
    {
        QuestionService = questionService;
        OutputCacheStore = outputCacheStore;
    }

    //[TypeFilter(typeof(LoggingActionFilter))]
    [HttpGet("{skip}/{take}")]
    [OutputCache(PolicyName = "QuestionsCache")]
    public async Task<PaginatedQuestionDto> GetAllQuestions(int skip, int take)
    {
        var questions = await QuestionService.GetAllAsync(skip, take);
        return questions;
    }

    [HttpGet("{id}")]
    public async Task<QuestionGetDto> GetById(long id)
    {
        var question = await QuestionService.GetByIdAsync(id);
        return question;
    }

    [HttpPost]
    public async Task<long> CreateQuestion(QuestionCreateDto questionCreateDto)
    {
        var questionId = await QuestionService.CreateAsync(questionCreateDto);
        return questionId;
    }


    [HttpDelete("{id}")]
    public async Task DeleteQuestion(long id)
    {
        await QuestionService.DeleteAsync(id);
        await OutputCacheStore.EvictByTagAsync("questions", default);
    }


    [HttpPut("{id}")]
    public async Task UpdateQuestion(long id, QuestionUpdateDto questionUpdateDto)
    {
        await QuestionService.UpdateAsync(id, questionUpdateDto);
    }
}
