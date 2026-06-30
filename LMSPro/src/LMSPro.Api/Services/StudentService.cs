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

public class StudentService : IStudentService
{
    private readonly IBaseRepository<Student> StudentRepository;
    private readonly IValidator<StudentCreateDto> StudentCreateDtoValidator;
    private readonly IValidator<StudentUpdateDto> StudentUpdateDtoValidator;
    private readonly RedisCacheService RedisCacheService;

    public StudentService(
        IBaseRepository<Student> studentRepository,
        IValidator<StudentCreateDto> studentCreateDtoValidator,
        IValidator<StudentUpdateDto> studentUpdateDtoValidator,
        RedisCacheService redisCacheService)
    {
        StudentRepository = studentRepository;
        StudentCreateDtoValidator = studentCreateDtoValidator;
        StudentUpdateDtoValidator = studentUpdateDtoValidator;
        RedisCacheService = redisCacheService;
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

        await RedisCacheService.RemoveAsync(CacheKeys.StudentsAll); 

        return student.StudentId;
    }

    public async Task<List<StudentGetDto>> GetAllAsync()
    {
        var cachedData = await RedisCacheService.GetAsync("students");
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedStudents = System.Text.Json.JsonSerializer.Deserialize<List<StudentGetDto>>(cachedData);
            return cachedStudents ?? new List<StudentGetDto>();
        }

        var students = await StudentRepository.GetAllQuery().ToListAsync();
        var res = students.Select(s => s.ToGetDto()).ToList();
        var jsonRes = System.Text.Json.JsonSerializer.Serialize(res);
        await RedisCacheService.SetAsync(CacheKeys.StudentsAll, jsonRes, GetDistributedCacheEntryOptions());
        return res;
    }

    public async Task<StudentGetDto> GetByIdAsync(long studentId)
    {
        var cachedData = await RedisCacheService.GetAsync(CacheKeys.StudentById(studentId));
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedStudent = System.Text.Json.JsonSerializer.Deserialize<StudentGetDto>(cachedData);
            if (cachedStudent != null)
                return cachedStudent;
        }

        var student = await StudentRepository.GetAllQuery()
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            throw new NotFoundException($"Student with ID {studentId} not found.");

        var dto = student.ToGetDto();
        var jsonDto = System.Text.Json.JsonSerializer.Serialize(dto);
        await RedisCacheService.SetAsync(CacheKeys.StudentById(studentId), jsonDto, GetDistributedCacheEntryOptions());

        return dto;
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

        await RedisCacheService.RemoveAsync(CacheKeys.StudentById(studentId));  
    }

    public async Task DeleteAsync(long studentId)
    {
        var student = await StudentRepository.GetAllQuery()
            .FirstOrDefaultAsync(s => s.StudentId == studentId);

        if (student == null)
            throw new NotFoundException($"Student with ID {studentId} not found to delete.");

        StudentRepository.Delete(student);
        await StudentRepository.SaveChangesAsync();

        await RedisCacheService.RemoveAsync(CacheKeys.StudentById(studentId));
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
