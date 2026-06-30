using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/resources")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService ResourceService;

    public ResourcesController(IResourceService resourceService)
    {
        ResourceService = resourceService;
    }

    [HttpGet]
    public async Task<List<ResourceGetDto>> GetAll()
    {
        return await ResourceService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ResourceGetDto> GetById(long id)
    {
        return await ResourceService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<long> Create(ResourceCreateDto resourceCreateDto)
    {
        return await ResourceService.CreateAsync(resourceCreateDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, ResourceUpdateDto resourceUpdateDto)
    {
        await ResourceService.UpdateAsync(id, resourceUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await ResourceService.DeleteAsync(id);
    }
}
