using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ILessonService
{
    Task<PaginatedLessonDto> GetAllAsync(int skip, int take);
    Task<LessonGetDto> GetByIdAsync(long lessonId);
    Task<long> CreateAsync(LessonCreateDto lessonCreateDto);
    Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto);
    Task DeleteAsync(long lessonId);
}