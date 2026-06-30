using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IStudentService
{
    Task<List<StudentGetDto>> GetAllAsync();
    Task<StudentGetDto> GetByIdAsync(long studentId);
    Task<long> CreateAsync(StudentCreateDto studentCreateDto);
    Task UpdateAsync(long studentId, StudentUpdateDto studentUpdateDto);
    Task DeleteAsync(long studentId);
}
