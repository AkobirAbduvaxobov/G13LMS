using FluentValidation;
using LMSPro.Api.Caching;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace LMSPro.Api.Services;

public class ExamService : IExamService
{
    private readonly IBaseRepository<Exam> ExamRepository;
    private readonly IValidator<ExamCreateDto> ExamCreateDtoValidator;
    private readonly IValidator<ExamUpdateDto> ExamUpdateDtoValidator;
    private readonly RedisCacheService RedisCacheService;
    public ExamService(
        IBaseRepository<Exam> examRepository,
        IValidator<ExamCreateDto> examCreateDtoValidator,
        IValidator<ExamUpdateDto> examUpdateDtoValidator,
        RedisCacheService redisCacheService)
    {
        ExamRepository = examRepository;
        ExamCreateDtoValidator = examCreateDtoValidator;
        ExamUpdateDtoValidator = examUpdateDtoValidator;
        RedisCacheService = redisCacheService;
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

        await RedisCacheService.RemoveAsync(CacheKeys.ExamsAll);

        return exam.ExamId;
    }

    public async Task<List<ExamGetDto>> GetAllAsync()
    {
        var cachedData = await RedisCacheService.GetAsync(CacheKeys.ExamsAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedExams = System.Text.Json.JsonSerializer.Deserialize<List<ExamGetDto>>(cachedData);
            return cachedExams ?? new List<ExamGetDto>();
        }

        var exams = await ExamRepository.GetAllQuery().ToListAsync();

        var res = exams.Select(e => e.ToGetDto()).ToList();
        var jsonData = System.Text.Json.JsonSerializer.Serialize(res);
        await RedisCacheService.SetAsync(CacheKeys.ExamsAll, jsonData, GetDistributedCacheEntryOptions());
        return res;
    }

    public async Task<ExamGetDto> GetByIdAsync(long examId)
    {
        var cachedData = await RedisCacheService.GetAsync(CacheKeys.StudentById(examId));
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedExam = System.Text.Json.JsonSerializer.Deserialize<ExamGetDto>(cachedData);
            if (cachedExam != null)
                return cachedExam;
        }

        var exam = await ExamRepository.GetAllQuery()
            .Include(e => e.Lesson)
            .FirstOrDefaultAsync(e => e.ExamId == examId);

        if (exam == null)
            throw new NotFoundException($"Exam with ID {examId} not found.");

        var res = exam.ToGetDto();
        var jsonData = System.Text.Json.JsonSerializer.Serialize(res);
        await RedisCacheService.SetAsync(CacheKeys.ExamById(examId), jsonData, GetDistributedCacheEntryOptions());

        return res;
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

        await RedisCacheService.RemoveAsync(CacheKeys.StudentById(examId));
    }

    public async Task DeleteAsync(long examId)
    {
        var exam = await ExamRepository.GetAllQuery()
            .FirstOrDefaultAsync(e => e.ExamId == examId);

        if (exam == null)
            throw new NotFoundException($"Exam with ID {examId} not found to delete.");

        ExamRepository.Delete(exam);
        await ExamRepository.SaveChangesAsync();

        await RedisCacheService.RemoveAsync(CacheKeys.StudentById(examId));
    }
    private DistributedCacheEntryOptions GetDistributedCacheEntryOptions()
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        return options;
    }
}
