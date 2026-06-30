using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ITeacherService
{
    Task<List<TeacherGetDto>> GetAllAsync();
    Task<TeacherGetDto> GetByIdAsync(long teacherId);
    Task<long> CreateAsync(TeacherCreateDto teacherCreateDto);
    Task UpdateAsync(long teacherId, TeacherUpdateDto teacherUpdateDto);
    Task DeleteAsync(long teacherId);
}
