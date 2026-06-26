using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IBaseRepository<Enrollment> EnrollmentRepository;

    public EnrollmentService(
        IBaseRepository<Enrollment> enrollmentRepository)
    {
        EnrollmentRepository = enrollmentRepository;
    }

    public async Task<long> CreateAsync(EnrollmentCreateDto dto)
    {
        var enrollment = dto.ToEntity();

        await EnrollmentRepository.AddAsync(enrollment);
        await EnrollmentRepository.SaveChangesAsync();

        return enrollment.EnrollmentId;
    }

    public async Task<List<EnrollmentGetDto>> GetAllAsync()
    {
        var enrollments = await EnrollmentRepository
            .GetAllQuery()
            .Include(x => x.Student)
            .Include(x => x.Course)
            .ToListAsync();

        return enrollments
            .Select(x => x.ToGetDto())
            .ToList();
    }

    public async Task<EnrollmentGetDto> GetByIdAsync(long id)
    {
        var enrollment = await EnrollmentRepository
            .GetAllQuery()
            .Include(x => x.Student)
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.EnrollmentId == id);

        if (enrollment == null)
            throw new NotFoundException($"Enrollment with ID {id} not found.");

        return enrollment.ToGetDto();
    }

    public async Task UpdateAsync(long id, EnrollmentUpdateDto dto)
    {
        var enrollment = await EnrollmentRepository
            .GetAllQuery()
            .FirstOrDefaultAsync(x => x.EnrollmentId == id);

        if (enrollment == null)
            throw new NotFoundException($"Enrollment with ID {id} not found.");

        dto.ToUpdateEntity(enrollment);

        EnrollmentRepository.Update(enrollment);
        await EnrollmentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var enrollment = await EnrollmentRepository
            .GetAllQuery()
            .FirstOrDefaultAsync(x => x.EnrollmentId == id);

        if (enrollment == null)
            throw new NotFoundException($"Enrollment with ID {id} not found.");

        EnrollmentRepository.Delete(enrollment);
        await EnrollmentRepository.SaveChangesAsync();
    }
}