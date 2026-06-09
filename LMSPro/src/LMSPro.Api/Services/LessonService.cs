using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class LessonService : ILessonService
{
    private readonly IBaseRepository<Lesson> LessonRepository;
    private readonly ILogger<LessonService> Logger;

    public LessonService(IBaseRepository<Lesson> lessonRepository, ILogger<LessonService> logger)
    {
        LessonRepository = lessonRepository;
        Logger = logger;
    }

    public async Task<long> CreateAsync(LessonCreateDto lessonCreateDto)
    {
        Logger.LogInformation("Creating new lesson. Title: {Title}", lessonCreateDto.Title);
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occurred while creating lesson.");
            throw;
        }
    }

    public async Task DeleteAsync(long lessonId)
    {
        Logger.LogInformation("Deleting lesson with ID: {LessonId}", lessonId);

        var lessonEntity = await LessonRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.LessonId == lessonId);
        if (lessonEntity == null)
        {
            throw new Exception($"Course with ID {lessonId} not found to delete.");
        }

        LessonRepository.Delete(lessonEntity);
        await LessonRepository.SaveChangesAsync();
        Logger.LogInformation("Lesson with ID {LessonId} deleted successfully.", lessonId);
    }

    public Task<PaginatedLessonDto> GetAllAsync(int skip, int take)
    {
        Logger.LogInformation(
            "Getting lessons. Skip: {Skip}, Take: {Take}",
            skip,
            take);

        throw new NotImplementedException();
    }

    public Task<LessonGetDto> GetByIdAsync(long lessonId)
    {
        Logger.LogInformation("Getting lesson by ID: {LessonId}", lessonId);
        throw new NotImplementedException();
    }

    public Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        Logger.LogInformation("Updating lesson with ID: {LessonId}", lessonId);

        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "Error occurred while updating lesson with ID: {LessonId}",
                lessonId);

            throw;
        }
    }

    private void ValidatePaginationParameters(ref int skip, ref int take)
    {
        Logger.LogDebug(
            "Validating pagination parameters. Skip: {Skip}, Take: {Take}",
            skip,
            take);

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
