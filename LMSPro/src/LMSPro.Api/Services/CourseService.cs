using LMSPro.Api.Dtos;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository CourseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        CourseRepository = courseRepository;
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
        var courseEntity = await CourseRepository.GetByIdAsync(courseId);
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
        var courseEntity = await CourseRepository.GetByIdAsync(courseId);
        if (courseEntity == null)
        {
            throw new Exception($"Course with ID {courseId} not found.");
        }

        return courseEntity.ToGetDto();
    }

    public async Task UpdateAsync(long courseId, CourseUpdateDto course)
    {
        var courseEntity = await CourseRepository.GetByIdAsync(courseId);
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
