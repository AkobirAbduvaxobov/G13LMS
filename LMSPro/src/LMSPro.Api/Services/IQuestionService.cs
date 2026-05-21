using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IQuestionService
{
    Task<PaginatedQuestionDto> GetAllAsync(int skip, int take);
    Task<QuestionGetDto> GetByIdAsync(long questionId);
    Task<long> CreateAsync(QuestionCreateDto questionCreateDto);
    Task UpdateAsync(long questionId, QuestionUpdateDto questionUpdateDto);
    Task DeleteAsync(long questionId);
}