using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ITeacherCourseService
{
    Task<List<TeacherCourseGetDto>> GetAllAsync();
    Task<TeacherCourseGetDto> GetByIdAsync(long teacherCourseId);
    Task<long> CreateAsync(TeacherCourseCreateDto dto);
    Task UpdateAsync(long teacherCourseId, TeacherCourseUpdateDto dto);
    Task DeleteAsync(long teacherCourseId);
}
