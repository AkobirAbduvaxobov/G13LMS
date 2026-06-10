using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LMSPro.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository CourseRepository;
    private readonly IBaseRepository<Enrollment> EnrollmentRepository;

    public CourseService(
        ICourseRepository courseRepository,
        IBaseRepository<Enrollment> enrollmentRepository)
    {
        CourseRepository = courseRepository;
        EnrollmentRepository = enrollmentRepository;
    }

    public async Task<long> CreateAsync(CourseCreateDto course)
    {
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
            throw new Exception($"Course with ID {courseId} not found to delete.");
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
            throw new Exception($"Course with ID {courseId} not found.");
        }

        var course = courseEntity.ToGetDto();

        return course;
    }

    public async Task UpdateAsync(long courseId, CourseUpdateDto course)
    {
        var courseEntity = await CourseRepository
            .GetAllQuery()
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (courseEntity == null)
        {
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
    }
}