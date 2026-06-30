using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class StudentService : IStudentService
{
    private readonly IBaseRepository<Student> StudentRepository;
    private readonly IValidator<StudentCreateDto> StudentCreateDtoValidator;
    private readonly IValidator<StudentUpdateDto> StudentUpdateDtoValidator;

    public StudentService(
        IBaseRepository<Student> studentRepository,
        IValidator<StudentCreateDto> studentCreateDtoValidator,
        IValidator<StudentUpdateDto> studentUpdateDtoValidator)
    {
        StudentRepository = studentRepository;
        StudentCreateDtoValidator = studentCreateDtoValidator;
        StudentUpdateDtoValidator = studentUpdateDtoValidator;
    }

    public async Task<long> CreateAsync(StudentCreateDto studentCreateDto)
    {
        var result = StudentCreateDtoValidator.Validate(studentCreateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var student = studentCreateDto.ToEntity();
        var emailExists = await StudentRepository.GetAllQuery()
            .AnyAsync(s => s.Email == studentCreateDto.Email);
        if (emailExists)
            throw new ConflictException($"A student with email '{studentCreateDto.Email}' already exists.");

        await StudentRepository.AddAsync(student);
        await StudentRepository.SaveChangesAsync();
        return student.StudentId;
    }

    public async Task<List<StudentGetDto>> GetAllAsync()
    {
        var students = await StudentRepository.GetAllQuery().ToListAsync();
        return students.Select(s => s.ToGetDto()).ToList();
    }

    public async Task<StudentGetDto> GetByIdAsync(long studentId)
    {
        var student = await StudentRepository.GetAllQuery()
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            throw new NotFoundException($"Student with ID {studentId} not found.");

        return student.ToGetDto();
    }

    public async Task UpdateAsync(long studentId, StudentUpdateDto studentUpdateDto)
    {
        var result = StudentUpdateDtoValidator.Validate(studentUpdateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var student = await StudentRepository.GetAllQuery()
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            throw new NotFoundException($"Student with ID {studentId} not found to update.");

        var emailTaken = await StudentRepository.GetAllQuery()
            .AnyAsync(s => s.Email == studentUpdateDto.Email && s.StudentId != studentId);
        if (emailTaken)
            throw new ConflictException($"A student with email '{studentUpdateDto.Email}' already exists.");

        studentUpdateDto.ToUpdateEntity(student);
        StudentRepository.Update(student);
        await StudentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long studentId)
    {
        var student = await StudentRepository.GetAllQuery()
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            throw new NotFoundException($"Student with ID {studentId} not found to delete.");

        StudentRepository.Delete(student);
        await StudentRepository.SaveChangesAsync();
    }
}
