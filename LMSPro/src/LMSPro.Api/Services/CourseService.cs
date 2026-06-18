using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LMSPro.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository CourseRepository;
    private readonly IBaseRepository<Enrollment> EnrollmentRepository;
    private readonly IValidator<CourseCreateDto> CourseCreateDtoValidator;
    private readonly IValidator<CourseUpdateDto> CourseUpdateDtoValidator;

    public CourseService(
        ICourseRepository courseRepository,
        IBaseRepository<Enrollment> enrollmentRepository,
        IValidator<CourseUpdateDto> courseUpdateDtoValidator,
        IValidator<CourseCreateDto> courseCreateDtoValidator)
    {
        CourseRepository = courseRepository;
        EnrollmentRepository = enrollmentRepository;
        CourseUpdateDtoValidator = courseUpdateDtoValidator;
        CourseCreateDtoValidator = courseCreateDtoValidator;
    }

    public async Task<long> CreateAsync(CourseCreateDto course)
    {
        var result = CourseCreateDtoValidator.Validate(course);

        if (!result.IsValid)
        {
            var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray());

            throw new LMSPro.Api.Exceptions.ValidationException(errors);
        }

        var courseEntity = course.ToEntity();

        await CourseRepository.AddAsync(courseEntity);
        await CourseRepository.SaveChangesAsync();


        return courseEntity.CourseId;
    }

    public async Task DeleteAsync(long courseId)
    {

        var courseEntity = await CourseRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (courseEntity == null)
        {
            throw new NotFoundException($"Course with ID {courseId} not found to delete.");
        }

        CourseRepository.Delete(courseEntity);
        await CourseRepository.SaveChangesAsync();

    }

    public async Task<List<CourseGetDto>> GetAllAsync()
    {

        var query = CourseRepository.GetAllQuery();

        var courseEntities = await query.ToListAsync();

        var courseDtos = courseEntities
                            .Select(c => c.ToGetDto())
                            .ToList();


        return courseDtos;
    }

    public async Task<CourseGetDto> GetByIdAsync(long courseId)
    {
        // eager loading

        var courseEntity = await CourseRepository
                            .GetAllQuery()
                            .Include(c => c.Lessons)
                            .Include(c => c.Enrollments)
                                .ThenInclude(e => e.Student)
                            .Include(c => c.TeacherCourses)
                                .ThenInclude(tc => tc.Teacher)
                            .FirstOrDefaultAsync(c => c.CourseId == courseId);
                            
        if (courseEntity == null)
        {
            throw new NotFoundException($"Course with ID {courseId} not found.");
        }


        return courseEntity.ToGetDto();
    }

    public async Task UpdateAsync(long courseId, CourseUpdateDto course)
    {

        var result = CourseUpdateDtoValidator.Validate(course);

        if (!result.IsValid)
        {
            var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray());

            throw new LMSPro.Api.Exceptions.ValidationException(errors);
        }

        var courseEntity = await CourseRepository
                            .GetAllQuery()
                            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (courseEntity == null)
        {
            throw new NotFoundException($"Course with ID {courseId} not found to update.");
        }

        courseEntity.Title = course.Title;
        courseEntity.Description = course.Description;
        courseEntity.Price = course.Price;
        courseEntity.DurationDays = course.DurationDays;
        courseEntity.AccessPeriodDays = course.AccessPeriodDays;
        courseEntity.IsActive = course.IsActive;

        CourseRepository.Update(courseEntity);
        await CourseRepository.SaveChangesAsync();

    }
}