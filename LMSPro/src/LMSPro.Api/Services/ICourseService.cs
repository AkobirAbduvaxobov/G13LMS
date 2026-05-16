using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ICourseService
{
    Task<List<CourseGetDto>> GetAllAsync();
    Task<CourseGetDto> GetByIdAsync(long courseId);
    Task<long> CreateAsync(CourseCreateDto course);
    Task UpdateAsync(long courseId, CourseUpdateDto course);
    Task DeleteAsync(long courseId);
}