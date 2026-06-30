using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class TeacherCourseService : ITeacherCourseService
{
    private readonly IBaseRepository<TeacherCourse> TeacherCourseRepository;

    public TeacherCourseService(IBaseRepository<TeacherCourse> teacherCourseRepository)
    {
        TeacherCourseRepository = teacherCourseRepository;
    }

    public async Task<long> CreateAsync(TeacherCourseCreateDto dto)
    {
        var exists = await TeacherCourseRepository
            .GetAllQuery()
            .AnyAsync(x => x.TeacherId == dto.TeacherId && x.CourseId == dto.CourseId);
        if (exists)
            throw new ConflictException($"Teacher {dto.TeacherId} is already assigned to course {dto.CourseId}.");

        var entity = dto.ToEntity();
        await TeacherCourseRepository.AddAsync(entity);
        await TeacherCourseRepository.SaveChangesAsync();
        return entity.TeacherCourseId;
    }

    public async Task<List<TeacherCourseGetDto>> GetAllAsync()
    {
        var items = await TeacherCourseRepository.GetAllQuery()
            .Include(x => x.Teacher)
            .Include(x => x.Course)
            .ToListAsync();

        return items.Select(x => x.ToGetDto()).ToList();
    }

    public async Task<TeacherCourseGetDto> GetByIdAsync(long teacherCourseId)
    {
        var entity = await TeacherCourseRepository.GetAllQuery()
            .Include(x => x.Teacher)
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.TeacherCourseId == teacherCourseId);

        if (entity == null)
            throw new NotFoundException($"TeacherCourse with ID {teacherCourseId} not found.");

        return entity.ToGetDto();
    }

    public async Task UpdateAsync(long teacherCourseId, TeacherCourseUpdateDto dto)
    {
        var entity = await TeacherCourseRepository.GetAllQuery()
            .FirstOrDefaultAsync(x => x.TeacherCourseId == teacherCourseId);

        if (entity == null)
            throw new NotFoundException($"TeacherCourse with ID {teacherCourseId} not found to update.");

        dto.ToUpdateEntity(entity);
        TeacherCourseRepository.Update(entity);
        await TeacherCourseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long teacherCourseId)
    {
        var entity = await TeacherCourseRepository.GetAllQuery()
            .FirstOrDefaultAsync(x => x.TeacherCourseId == teacherCourseId);

        if (entity == null)
            throw new NotFoundException($"TeacherCourse with ID {teacherCourseId} not found to delete.");

        TeacherCourseRepository.Delete(entity);
        await TeacherCourseRepository.SaveChangesAsync();
    }
}
