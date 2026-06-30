using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class ResourceMapper
{
    public static Resource ToEntity(this ResourceCreateDto dto)
    {
        return new Resource
        {
            Name = dto.Name,
            Url = dto.Url,
            Type = dto.Type,
            LessonId = dto.LessonId
        };
    }

    public static ResourceGetDto ToGetDto(this Resource resource)
    {
        return new ResourceGetDto
        {
            ResourceId = resource.ResourceId,
            Name = resource.Name,
            Url = resource.Url,
            Type = resource.Type,
            LessonId = resource.LessonId
        };
    }

    public static void ToUpdateEntity(this ResourceUpdateDto dto, Resource resource)
    {
        resource.Name = dto.Name;
        resource.Url = dto.Url;
        resource.Type = dto.Type;
        resource.LessonId = dto.LessonId;
    }
}
