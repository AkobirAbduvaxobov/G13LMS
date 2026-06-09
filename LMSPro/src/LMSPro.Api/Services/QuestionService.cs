using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class QuestionService : IQuestionService
{
    private readonly IBaseRepository<Question> QuestionRepository;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(
        IBaseRepository<Question> questionRepository,
        ILogger<QuestionService> logger)
    {
        QuestionRepository = questionRepository;
        _logger = logger;
    }

    public async Task<long> CreateAsync(QuestionCreateDto questionCreateDto)
    {
        var question = questionCreateDto.ToEntity();
        await QuestionRepository.AddAsync(question);
        await QuestionRepository.SaveChangesAsync();

        return question.QuestionId;
    }

    public async Task DeleteAsync(long questionId)
    {
        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            _logger.LogWarning("Question with ID: {QuestionId} not found to delete", questionId);
            throw new Exception($"Question with ID {questionId} not found to delete.");
        }

        QuestionRepository.Delete(question);
        await QuestionRepository.SaveChangesAsync();
    }

    public async Task<PaginatedQuestionDto> GetAllAsync(int skip = 0, int take = 20)
    {
        if (skip < 0) skip = 0;
        if (take > 20) take = 20;

        var query = QuestionRepository.GetAllQuery();
        query = query.Skip(skip).Take(take);
        var questions = await query.ToListAsync();

        var questionCount = await QuestionRepository.GetAllQuery().CountAsync();

        var questionDtos = questions.Select(q => q.ToGetDto()).ToList();

        var result = new PaginatedQuestionDto
        {
            TotalCount = questionCount,
            QuestionGetDtos = questionDtos
        };

        return result;
    }

    public async Task<QuestionGetDto> GetByIdAsync(long questionId)
    {
        var question = await QuestionRepository.GetAllQuery()
                            .Include(q => q.Lesson)
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            _logger.LogWarning("Question with ID: {QuestionId} not found", questionId);
            throw new Exception($"Question with ID {questionId} not found.");
        }

        var questionDto = question.ToGetDto();

        return questionDto;
    }

    public async Task UpdateAsync(long questionId, QuestionUpdateDto questionUpdateDto)
    {
        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            _logger.LogWarning("Question with ID: {QuestionId} not found to update", questionId);
            throw new Exception($"Question with ID {questionId} not found to update.");
        }

        questionUpdateDto.UpdateEntity(question);
        await QuestionRepository.SaveChangesAsync();
    }
}
