using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IEnrollmentService
{
    Task<long> CreateAsync(EnrollmentCreateDto enrollmentCreateDto);
    Task<List<EnrollmentGetDto>> GetAllAsync();
    Task<EnrollmentGetDto> GetByIdAsync(long id);
    Task UpdateAsync(long id, EnrollmentUpdateDto enrollmentUpdateDto);
    Task DeleteAsync(long id);
}