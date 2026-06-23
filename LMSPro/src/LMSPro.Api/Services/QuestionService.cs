using LMSPro.Api.Caching;
using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace LMSPro.Api.Services;

public class QuestionService : IQuestionService
{
    private readonly IBaseRepository<Question> QuestionRepository;
    private readonly CacheSettings CacheSettings;
    private readonly IMemoryCache MemoryCache;
    public QuestionService(
    IBaseRepository<Question> questionRepository,
    CacheSettings cacheSettings,
    IMemoryCache memoryCache)
    {
        QuestionRepository = questionRepository;
        CacheSettings = cacheSettings;
        MemoryCache = memoryCache;
    }

    public async Task<long> CreateAsync(QuestionCreateDto questionCreateDto)
    {

        var question = questionCreateDto.ToEntity();
        await QuestionRepository.AddAsync(question);
        await QuestionRepository.SaveChangesAsync();

        InvalidateQuestionsCache(question.QuestionId);

        return question.QuestionId;
    }

    public async Task DeleteAsync(long questionId)
    {

        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            throw new NotFoundException($"Question with ID {questionId} not found to delete.");
        }

        QuestionRepository.Delete(question);
        await QuestionRepository.SaveChangesAsync();

        InvalidateQuestionsCache(questionId);

    }

    public async Task<PaginatedQuestionDto> GetAllAsync(int skip = 0, int take = 20)
    {
        var cacheKey = CacheKeys.QuestionsAll(skip, take);

        if (MemoryCache.TryGetValue(
            cacheKey,
            out PaginatedQuestionDto? cachedQuestions))
        {
            return cachedQuestions!;
        }
        if (skip < 0) skip = 0;
        if (take > 20) take = 20;

        var query = QuestionRepository.GetAllQuery();
        query = query.Skip(skip).Take(take);
        var questions = await query.ToListAsync();

        var questionCount = await QuestionRepository.GetAllQuery().CountAsync();


        var questionDtos = questions.Select(q => q.ToGetDto()).ToList();

        var result = new PaginatedQuestionDto
        {
            TotalCount = questionCount,
            QuestionGetDtos = questionDtos
        };

        MemoryCache.Set(
        CacheKeys.QuestionsAll,
        result,
        GetQuestionCacheOptions());

        return result;
    }

    public async Task<QuestionGetDto> GetByIdAsync(long questionId)
    {
        var cacheKey = CacheKeys.QuestionById(questionId);

        if (MemoryCache.TryGetValue(
            cacheKey,
            out QuestionGetDto? cachedQuestion))
        {
            return cachedQuestion!;
        }

        var question = await QuestionRepository.GetAllQuery()
                            .Include(q => q.Lesson)
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            throw new NotFoundException($"Question with ID {questionId} not found.");
        }
        var questionDto = question.ToGetDto();

        MemoryCache.Set(
            cacheKey,
            questionDto,
            GetQuestionCacheOptions());

        return questionDto;

     
    }

    public async Task UpdateAsync(long questionId, QuestionUpdateDto questionUpdateDto)
    {

        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            throw new NotFoundException($"Question with ID {questionId} not found to update.");
        }

        questionUpdateDto.UpdateEntity(question);
        await QuestionRepository.SaveChangesAsync();

        InvalidateQuestionsCache(question.QuestionId);

    }

    private MemoryCacheEntryOptions GetQuestionCacheOptions()
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                CacheSettings.Questions.AbsoluteExpirationMinutes),

            SlidingExpiration = TimeSpan.FromMinutes(
                CacheSettings.Questions.SlidingExpirationMinutes)
        };
    }

    private void InvalidateQuestionsCache(long questionId)
    {
     
        MemoryCache.Remove(CacheKeys.QuestionById(questionId));
    }
}