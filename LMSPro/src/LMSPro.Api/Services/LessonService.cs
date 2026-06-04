using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class LessonService : ILessonService
{
    private readonly IBaseRepository<Lesson> LessonRepository;

    public LessonService(IBaseRepository<Lesson> lessonRepository)
    {
        LessonRepository = lessonRepository;
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

    public Task<LessonGetDto> GetByIdAsync(long lessonId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        var lessonEntity = await LessonRepository
            .GetAllQuery()
            .FirstOrDefaultAsync(l => l.LessonId == lessonId);

        if (lessonEntity == null)
        {
            throw new Exception($"Lesson with ID {lessonId} not found.");
        }

        lessonEntity.Title = lessonUpdateDto.Title;
        lessonEntity.Content = lessonUpdateDto.Content;
        lessonEntity.Order = lessonUpdateDto.Order;
        lessonEntity.Duration = lessonUpdateDto.Duration;

        await LessonRepository.SaveChangesAsync();
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
