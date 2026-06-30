using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IResourceService
{
    Task<List<ResourceGetDto>> GetAllAsync();
    Task<ResourceGetDto> GetByIdAsync(long resourceId);
    Task<long> CreateAsync(ResourceCreateDto resourceCreateDto);
    Task UpdateAsync(long resourceId, ResourceUpdateDto resourceUpdateDto);
    Task DeleteAsync(long resourceId);
}
