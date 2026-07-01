using FluentValidation;
using LMSPro.Api.Caching;
using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LMSPro.Api.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository CourseRepository;
    private readonly IBaseRepository<Enrollment> EnrollmentRepository;
    private readonly IValidator<CourseCreateDto> CourseCreateDtoValidator;
    private readonly IValidator<CourseUpdateDto> CourseUpdateDtoValidator;
    private readonly CacheSettings CacheSettings;
    private readonly IMemoryCache MemoryCache;

    public CourseService(
        ICourseRepository courseRepository,
        IBaseRepository<Enrollment> enrollmentRepository,
        IValidator<CourseUpdateDto> courseUpdateDtoValidator,
        IValidator<CourseCreateDto> courseCreateDtoValidator,
        CacheSettings cacheSettings,
        IMemoryCache memoryCache)
    {
        CourseRepository = courseRepository;
        EnrollmentRepository = enrollmentRepository;
        CourseUpdateDtoValidator = courseUpdateDtoValidator;
        CourseCreateDtoValidator = courseCreateDtoValidator;
        CacheSettings = cacheSettings;
        MemoryCache = memoryCache;
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

        InvalidateCoursesCache(courseEntity.CourseId);

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
        InvalidateCoursesCache(courseEntity.CourseId);
    }

    public async Task<List<CourseGetDto>> GetAllAsync()
    {
        List<CourseGetDto>? cachedCourses = new List<CourseGetDto>();
        if (MemoryCache.TryGetValue(CacheKeys.CoursesAll, out cachedCourses))
        {
            return cachedCourses!;
        }

        var query = CourseRepository.GetAllQuery();

        var courseEntities = await query.ToListAsync();

        var courseDtos = courseEntities
                            .Select(c => c.ToGetDto())
                            .ToList();


        MemoryCache.Set(CacheKeys.CoursesAll, courseDtos, GetCourseCacheOptions());

        return courseDtos;
    }

    public async Task<CourseGetDto> GetByIdAsync(long courseId) 
    {
        var cacheKey = CacheKeys.CourseById(courseId); 

        if (MemoryCache.TryGetValue(cacheKey, out CourseGetDto? cachedCourse))
        {
            return cachedCourse!;
        }

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
        var dto = courseEntity.ToGetDto();
        MemoryCache.Set(cacheKey, dto, GetCourseCacheOptions());
        return dto;
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

        course.ToUpdateEntity(courseEntity);

        CourseRepository.Update(courseEntity);
        await CourseRepository.SaveChangesAsync();
        InvalidateCoursesCache(courseEntity.CourseId);
        
        
        
        
        //cesceadcdscsddscdsc
        
        
        
        //csecdscdeccsecdc
        
        
        ////csdcdscdscdscdsc
    }

    private MemoryCacheEntryOptions GetCourseCacheOptions()
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                CacheSettings.Courses.AbsoluteExpirationMinutes),

            SlidingExpiration = TimeSpan.FromMinutes(
                CacheSettings.Courses.SlidingExpirationMinutes)
        };
    }

    private void InvalidateCoursesCache(long courseId)
    {
        MemoryCache.Remove(CacheKeys.CoursesAll);
        MemoryCache.Remove(CacheKeys.CourseById(courseId));
    }
}