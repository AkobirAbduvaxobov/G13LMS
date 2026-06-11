using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;
namespace LMSPro.Api.Services;

public class LessonService : ILessonService
{
    private readonly IBaseRepository<Lesson> LessonRepository;
    private readonly ILogger<QuestionService> Logger;

    public LessonService(IBaseRepository<Lesson> lessonRepository, 
        ILogger<QuestionService> logger)
    {
        LessonRepository = lessonRepository;
        Logger = logger;
    }

    public async Task<long> CreateAsync(LessonCreateDto lessonCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(long lessonId)
    {
        var lessonEntity = await LessonRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.LessonId == lessonId);
        if (lessonEntity == null)
        {
            throw new Exception($"Course with ID {lessonId} not found to delete.");
        }

        LessonRepository.Delete(lessonEntity);
        await LessonRepository.SaveChangesAsync();
    }

    public Task<PaginatedLessonDto> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public async Task<LessonGetDto> GetByIdAsync(long lessonId)
    {
        Logger.LogInformation("Getting lesson by ID: {LessonId}", lessonId);

        var lessonEntity = await LessonRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.LessonId == lessonId);
        if (lessonEntity == null)
        {
            Logger.LogWarning("Lesson with ID: {LessonId} not found", lessonId);
            throw new Exception($"Lesson with ID {lessonId} not found.");
        }

        var lessonDto = lessonEntity.ToGetDto();

        Logger.LogInformation("Lesson with ID: {LessonId} retrieved successfully", lessonId);
        return lessonDto;
    }

    public Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        throw new NotImplementedException();
    }

    private void ValidatePaginationParameters(ref int skip, ref int take)
    {
        var res = skip + take;
        while (true)
        {
            if (skip < 0) skip = 0;
            if (take > 20) take = 20;
            if (res > 20)
            {
                take = 20 - skip;
                res = skip + take;
            }
            else
            {
                break;
            }
        }
        if (skip < 0) skip = 0;
        if (take > 20) take = 20;
    }
}
