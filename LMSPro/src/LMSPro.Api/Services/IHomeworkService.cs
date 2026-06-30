using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IHomeworkService
{
    Task<List<HomeworkGetDto>> GetAllAsync();
    Task<HomeworkGetDto> GetByIdAsync(long homeworkId);
    Task<long> CreateAsync(HomeworkCreateDto homeworkCreateDto);
    Task UpdateAsync(long homeworkId, HomeworkUpdateDto homeworkUpdateDto);
    Task DeleteAsync(long homeworkId);
}
