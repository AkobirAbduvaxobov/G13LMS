using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace LMSPro.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository CourseRepository;
    private readonly IBaseRepository<Enrollment> EnrollmentRepository;
    private readonly ILogger<CourseService> Logger;

    public CourseService(ICourseRepository courseRepository, IBaseRepository<Enrollment> enrollmentRepository, ILogger<CourseService> logger)
    {
        CourseRepository = courseRepository;
        EnrollmentRepository = enrollmentRepository;
        Logger = logger;
    }

    public async Task<long> CreateAsync(CourseCreateDto course)
    {
        Logger.LogInformation("CreateAsync started");

        var courseEntity = course.ToEntity();

        await CourseRepository.AddAsync(courseEntity);
        await CourseRepository.SaveChangesAsync();

        Logger.LogInformation("CreateAsync finished. CourseId: {CourseId}", courseEntity.CourseId);

        return courseEntity.CourseId;
    }

    public async Task DeleteAsync(long courseId)
    {
        Logger.LogInformation("DeleteAsync started. Id: {CourseId}", courseId);

        var courseEntity = await CourseRepository
        .GetAllQuery()
        .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (courseEntity == null)
        {
            Logger.LogWarning("Course not found for delete. Id: {CourseId}", courseId);
            throw new Exception($"Course with ID {courseId} not found to delete.");
        }

        CourseRepository.Delete(courseEntity);
        await CourseRepository.SaveChangesAsync();

        Logger.LogInformation("DeleteAsync finished. Id: {CourseId}", courseId);
    }

    public async Task<List<CourseGetDto>> GetAllAsync()
    {
        Logger.LogInformation("GetAllAsync started");

        var query = CourseRepository.GetAllQuery();

        var courseEntities = await query.ToListAsync();

        var courseDtos = courseEntities
        .Select(c => c.ToGetDto())
        .ToList();

        Logger.LogInformation("GetAllAsync finished. Count: {Count}", courseDtos.Count);

        return courseDtos;
    }

    public async Task<CourseGetDto> GetByIdAsync(long courseId)
    {
        Logger.LogInformation("GetByIdAsync started. Id: {CourseId}", courseId);

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
            Logger.LogWarning("Course not found. Id: {CourseId}", courseId);
            throw new Exception($"Course with ID {courseId} not found.");
        }

        var course = courseEntity.ToGetDto();

        Logger.LogInformation("GetByIdAsync finished. Id: {CourseId}", courseId);

        return course;
    }

    public async Task UpdateAsync(long courseId, CourseUpdateDto course)
    {
        Logger.LogInformation("UpdateAsync started. Id: {CourseId}", courseId);

        var courseEntity = await CourseRepository
        .GetAllQuery()
        .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (courseEntity == null)
        {
            Logger.LogWarning("Course not found for update. Id: {CourseId}", courseId);
            throw new Exception($"Course with ID {courseId} not found to update.");
        }

        courseEntity.Title = course.Title;
        courseEntity.Description = course.Description;
        courseEntity.Price = course.Price;
        courseEntity.DurationDays = course.DurationDays;
        courseEntity.AccessPeriodDays = course.AccessPeriodDays;
        courseEntity.IsActive = course.IsActive;

        CourseRepository.Update(courseEntity);
        await CourseRepository.SaveChangesAsync();

        Logger.LogInformation("UpdateAsync finished. Id: {CourseId}", courseId);
    }
}