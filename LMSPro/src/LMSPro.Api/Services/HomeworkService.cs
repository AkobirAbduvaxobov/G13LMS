using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IBaseRepository<Homework> HomeworkRepository;
    private readonly IValidator<HomeworkCreateDto> HomeworkCreateDtoValidator;
    private readonly IValidator<HomeworkUpdateDto> HomeworkUpdateDtoValidator;

    public HomeworkService(
        IBaseRepository<Homework> homeworkRepository,
        IValidator<HomeworkCreateDto> homeworkCreateDtoValidator,
        IValidator<HomeworkUpdateDto> homeworkUpdateDtoValidator)
    {
        HomeworkRepository = homeworkRepository;
        HomeworkCreateDtoValidator = homeworkCreateDtoValidator;
        HomeworkUpdateDtoValidator = homeworkUpdateDtoValidator;
    }

    public async Task<long> CreateAsync(HomeworkCreateDto homeworkCreateDto)
    {
        var result = HomeworkCreateDtoValidator.Validate(homeworkCreateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var homework = homeworkCreateDto.ToEntity();
        await HomeworkRepository.AddAsync(homework);
        await HomeworkRepository.SaveChangesAsync();
        return homework.HomeworkId;
    }

    public async Task<List<HomeworkGetDto>> GetAllAsync()
    {
        var homeworks = await HomeworkRepository.GetAllQuery().ToListAsync();
        return homeworks.Select(h => h.ToGetDto()).ToList();
    }

    public async Task<HomeworkGetDto> GetByIdAsync(long homeworkId)
    {
        var homework = await HomeworkRepository.GetAllQuery()
            .Include(h => h.Lesson)
            .FirstOrDefaultAsync(h => h.HomeworkId == homeworkId);

        if (homework == null)
            throw new NotFoundException($"Homework with ID {homeworkId} not found.");

        return homework.ToGetDto();
    }

    public async Task UpdateAsync(long homeworkId, HomeworkUpdateDto homeworkUpdateDto)
    {
        var result = HomeworkUpdateDtoValidator.Validate(homeworkUpdateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var homework = await HomeworkRepository.GetAllQuery()
            .FirstOrDefaultAsync(h => h.HomeworkId == homeworkId);

        if (homework == null)
            throw new NotFoundException($"Homework with ID {homeworkId} not found to update.");

        homeworkUpdateDto.ToUpdateEntity(homework);
        HomeworkRepository.Update(homework);
        await HomeworkRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long homeworkId)
    {
        var homework = await HomeworkRepository.GetAllQuery()
            .FirstOrDefaultAsync(h => h.HomeworkId == homeworkId);

        if (homework == null)
            throw new NotFoundException($"Homework with ID {homeworkId} not found to delete.");

        HomeworkRepository.Delete(homework);
        await HomeworkRepository.SaveChangesAsync();
    }
}
