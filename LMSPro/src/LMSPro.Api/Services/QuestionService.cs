using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class QuestionService : IQuestionService
{
    private readonly IBaseRepository<Question> QuestionRepository;
    private readonly ILogger<QuestionService> Logger;

    public QuestionService(
        IBaseRepository<Question> questionRepository,
        ILogger<QuestionService> logger)
    {
        QuestionRepository = questionRepository;
        Logger = logger;
    }

    public async Task<long> CreateAsync(QuestionCreateDto questionCreateDto)
    {
        Logger.LogInformation("Creating a new question...");

        var question = questionCreateDto.ToEntity();
        await QuestionRepository.AddAsync(question);
        await QuestionRepository.SaveChangesAsync();

        Logger.LogInformation("Question created successfully with ID: {QuestionId}", question.QuestionId);

        return question.QuestionId;
    }

    public async Task DeleteAsync(long questionId)
    {
        Logger.LogInformation("Deleting question with ID: {QuestionId}", questionId);

        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            Logger.LogWarning("Question with ID: {QuestionId} not found to delete", questionId);
            throw new Exception($"Question with ID {questionId} not found to delete.");
        }

        QuestionRepository.Delete(question);
        await QuestionRepository.SaveChangesAsync();

        Logger.LogInformation("Question with ID: {QuestionId} deleted successfully", questionId);
    }

    public async Task<PaginatedQuestionDto> GetAllAsync(int skip = 0, int take = 20)
    {
        Logger.LogInformation("Getting all questions with skip: {Skip}, take: {Take}", skip, take);

        if (skip < 0) skip = 0;
        if (take > 20) take = 20;

        var query = QuestionRepository.GetAllQuery();
        query = query.Skip(skip).Take(take);
        var questions = await query.ToListAsync();

        var questionCount = await QuestionRepository.GetAllQuery().CountAsync();

        Logger.LogInformation("Retrieved {Count} questions out of {Total} total", questions.Count, questionCount);

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
        Logger.LogInformation("Getting question by ID: {QuestionId}", questionId);

        var question = await QuestionRepository.GetAllQuery()
                            .Include(q => q.Lesson)
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            Logger.LogWarning("Question with ID: {QuestionId} not found", questionId);
            throw new Exception($"Question with ID {questionId} not found.");
        }

        var questionDto = question.ToGetDto();

        Logger.LogInformation("Question with ID: {QuestionId} retrieved successfully", questionId);

        return questionDto;
    }

    public async Task UpdateAsync(long questionId, QuestionUpdateDto questionUpdateDto)
    {
        Logger.LogInformation("Updating question with ID: {QuestionId}", questionId);

        var question = await QuestionRepository.GetAllQuery()
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
        {
            Logger.LogWarning("Question with ID: {QuestionId} not found to update", questionId);
            throw new Exception($"Question with ID {questionId} not found to update.");
        }

        questionUpdateDto.UpdateEntity(question);
        await QuestionRepository.SaveChangesAsync();

        Logger.LogInformation("Question with ID: {QuestionId} updated successfully", questionId);
    }
}

