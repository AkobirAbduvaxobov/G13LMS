using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;
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
        var lessonEntity = lessonCreateDto.ToEntity();
        await LessonRepository.AddAsync(lessonEntity);
        await LessonRepository.SaveChangesAsync();
        return lessonEntity.LessonId;
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

        var lessonEntity = await LessonRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.LessonId == lessonId);
        if (lessonEntity == null)
        {
            throw new Exception($"Lesson with ID {lessonId} not found.");
        }

        var lessonDto = lessonEntity.ToGetDto();

        return lessonDto;
    }

    public async Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        var lessonEntity = await LessonRepository
            .GetAllQuery()
            .FirstOrDefaultAsync(c => c.LessonId == lessonId);

        if (lessonEntity == null)
        {
            throw new NotFoundException($"Lesson with ID {lessonId} not found to update.");
        }

        lessonEntity.Title = lessonUpdateDto.Title;
        lessonEntity.Content = lessonUpdateDto.Content;
        lessonEntity.Order = lessonUpdateDto.Order;
        lessonEntity.Duration = lessonUpdateDto.Duration;
        lessonEntity.CourseId = lessonUpdateDto.CourseId;

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
