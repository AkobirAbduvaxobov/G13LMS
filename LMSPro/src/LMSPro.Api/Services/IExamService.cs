using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IExamService
{
    Task<List<ExamGetDto>> GetAllAsync();
    Task<ExamGetDto> GetByIdAsync(long examId);
    Task<long> CreateAsync(ExamCreateDto examCreateDto);
    Task UpdateAsync(long examId, ExamUpdateDto examUpdateDto);
    Task DeleteAsync(long examId);
}
