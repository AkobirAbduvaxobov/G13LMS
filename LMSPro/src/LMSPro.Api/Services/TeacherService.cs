using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class TeacherService : ITeacherService
{
    private readonly IBaseRepository<Teacher> TeacherRepository;
    private readonly IValidator<TeacherCreateDto> TeacherCreateDtoValidator;
    private readonly IValidator<TeacherUpdateDto> TeacherUpdateDtoValidator;

    public TeacherService(
        IBaseRepository<Teacher> teacherRepository,
        IValidator<TeacherCreateDto> teacherCreateDtoValidator,
        IValidator<TeacherUpdateDto> teacherUpdateDtoValidator)
    {
        TeacherRepository = teacherRepository;
        TeacherCreateDtoValidator = teacherCreateDtoValidator;
        TeacherUpdateDtoValidator = teacherUpdateDtoValidator;
    }

    public async Task<long> CreateAsync(TeacherCreateDto teacherCreateDto)
    {
        var result = TeacherCreateDtoValidator.Validate(teacherCreateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var teacher = teacherCreateDto.ToEntity();
        await TeacherRepository.AddAsync(teacher);
        await TeacherRepository.SaveChangesAsync();
        return teacher.TeacherId;
    }

    public async Task<List<TeacherGetDto>> GetAllAsync()
    {
        var teachers = await TeacherRepository.GetAllQuery().ToListAsync();
        return teachers.Select(t => t.ToGetDto()).ToList();
    }

    public async Task<TeacherGetDto> GetByIdAsync(long teacherId)
    {
        var teacher = await TeacherRepository.GetAllQuery()
            .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

        if (teacher == null)
            throw new NotFoundException($"Teacher with ID {teacherId} not found.");

        return teacher.ToGetDto();
    }

    public async Task UpdateAsync(long teacherId, TeacherUpdateDto teacherUpdateDto)
    {
        var result = TeacherUpdateDtoValidator.Validate(teacherUpdateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var teacher = await TeacherRepository.GetAllQuery()
            .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

        if (teacher == null)
            throw new NotFoundException($"Teacher with ID {teacherId} not found to update.");

        teacherUpdateDto.ToUpdateEntity(teacher);
        TeacherRepository.Update(teacher);
        await TeacherRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long teacherId)
    {
        var teacher = await TeacherRepository.GetAllQuery()
            .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

        if (teacher == null)
            throw new NotFoundException($"Teacher with ID {teacherId} not found to delete.");

        TeacherRepository.Delete(teacher);
        await TeacherRepository.SaveChangesAsync();
    }
}
