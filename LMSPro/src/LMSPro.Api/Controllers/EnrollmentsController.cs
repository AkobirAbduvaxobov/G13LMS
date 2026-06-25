using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/enrollments")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService EnrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        EnrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<long> Create(EnrollmentCreateDto enrollmentCreateDto)
    {
        var enrollmentId = await EnrollmentService.CreateAsync(enrollmentCreateDto);
        return enrollmentId;
    }

    [HttpGet]
    public async Task<List<EnrollmentGetDto>> GetAll()
    {
        var enrollments = await EnrollmentService.GetAllAsync();
        return enrollments;
    }

    [HttpGet("{id}")]
    public async Task<EnrollmentGetDto> GetById(long id)
    {
        var enrollment = await EnrollmentService.GetByIdAsync(id);
        return enrollment;
    }

    [HttpPut("{id}")]
    public async Task Update(long id, EnrollmentUpdateDto enrollmentUpdateDto)
    {
        await EnrollmentService.UpdateAsync(id, enrollmentUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await EnrollmentService.DeleteAsync(id);
    }
}