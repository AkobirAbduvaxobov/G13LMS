using FluentValidation;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Mappings;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class ResourceService : IResourceService
{
    private readonly IBaseRepository<Resource> ResourceRepository;
    private readonly IValidator<ResourceCreateDto> ResourceCreateDtoValidator;

    public ResourceService(
        IBaseRepository<Resource> resourceRepository,
        IValidator<ResourceCreateDto> resourceCreateDtoValidator)
    {
        ResourceRepository = resourceRepository;
        ResourceCreateDtoValidator = resourceCreateDtoValidator;
    }

    public async Task<long> CreateAsync(ResourceCreateDto resourceCreateDto)
    {
        var result = ResourceCreateDtoValidator.Validate(resourceCreateDto);
        if (!result.IsValid)
        {
            throw new Exceptions.ValidationException(result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray()));
        }

        var resource = resourceCreateDto.ToEntity();
        await ResourceRepository.AddAsync(resource);
        await ResourceRepository.SaveChangesAsync();
        return resource.ResourceId;
    }

    public async Task<List<ResourceGetDto>> GetAllAsync()
    {
        var resources = await ResourceRepository.GetAllQuery().ToListAsync();
        return resources.Select(r => r.ToGetDto()).ToList();
    }

    public async Task<ResourceGetDto> GetByIdAsync(long resourceId)
    {
        var resource = await ResourceRepository.GetAllQuery()
            .FirstOrDefaultAsync(r => r.ResourceId == resourceId);

        if (resource == null)
            throw new NotFoundException($"Resource with ID {resourceId} not found.");

        return resource.ToGetDto();
    }

    public async Task UpdateAsync(long resourceId, ResourceUpdateDto resourceUpdateDto)
    {
        var resource = await ResourceRepository.GetAllQuery()
            .FirstOrDefaultAsync(r => r.ResourceId == resourceId);

        if (resource == null)
            throw new NotFoundException($"Resource with ID {resourceId} not found to update.");

        resourceUpdateDto.ToUpdateEntity(resource);
        ResourceRepository.Update(resource);
        await ResourceRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long resourceId)
    {
        var resource = await ResourceRepository.GetAllQuery()
            .FirstOrDefaultAsync(r => r.ResourceId == resourceId);

        if (resource == null)
            throw new NotFoundException($"Resource with ID {resourceId} not found to delete.");

        ResourceRepository.Delete(resource);
        await ResourceRepository.SaveChangesAsync();
    }
}
