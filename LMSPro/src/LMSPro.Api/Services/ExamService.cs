using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class ExamService : IExamService
{
    private readonly IBaseRepository<Exam> ExamRepository;
    private readonly IValidator<ExamCreateDto> ExamCreateDtoValidator;
    private readonly IValidator<ExamUpdateDto> ExamUpdateDtoValidator;

    public ExamService(
        IBaseRepository<Exam> examRepository,
        IValidator<ExamCreateDto> examCreateDtoValidator,
        IValidator<ExamUpdateDto> examUpdateDtoValidator)
    {
        ExamRepository = examRepository;
        ExamCreateDtoValidator = examCreateDtoValidator;
        ExamUpdateDtoValidator = examUpdateDtoValidator;
    }

    public async Task<long> CreateAsync(ExamCreateDto examCreateDto)
    {
        var result = ExamCreateDtoValidator.Validate(examCreateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var exam = examCreateDto.ToEntity();
        await ExamRepository.AddAsync(exam);
        await ExamRepository.SaveChangesAsync();
        return exam.ExamId;
    }

    public async Task<List<ExamGetDto>> GetAllAsync()
    {
        var exams = await ExamRepository.GetAllQuery().ToListAsync();
        return exams.Select(e => e.ToGetDto()).ToList();
    }

    public async Task<ExamGetDto> GetByIdAsync(long examId)
    {
        var exam = await ExamRepository.GetAllQuery()
            .Include(e => e.Lesson)
            .FirstOrDefaultAsync(e => e.ExamId == examId);

        if (exam == null)
            throw new NotFoundException($"Exam with ID {examId} not found.");

        return exam.ToGetDto();
    }

    public async Task UpdateAsync(long examId, ExamUpdateDto examUpdateDto)
    {
        var result = ExamUpdateDtoValidator.Validate(examUpdateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var exam = await ExamRepository.GetAllQuery()
            .FirstOrDefaultAsync(e => e.ExamId == examId);

        if (exam == null)
            throw new NotFoundException($"Exam with ID {examId} not found to update.");

        examUpdateDto.ToUpdateEntity(exam);
        ExamRepository.Update(exam);
        await ExamRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long examId)
    {
        var exam = await ExamRepository.GetAllQuery()
            .FirstOrDefaultAsync(e => e.ExamId == examId);

        if (exam == null)
            throw new NotFoundException($"Exam with ID {examId} not found to delete.");

        ExamRepository.Delete(exam);
        await ExamRepository.SaveChangesAsync();
    }
}
