using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class ResourceMapper
{
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

    public static Resource ToEntity(this ResourceCreateDto resourceCreateDto)
    {
        return new Resource
        {
            Name = resourceCreateDto.Name,
            Url = resourceCreateDto.Url,
            Type = resourceCreateDto.Type,
            LessonId = resourceCreateDto.LessonId
        };
    }

    public static void UpdateEntity(this ResourceUpdateDto resourceUpdateDto, Resource resource)
    {
        resource.Name = resourceUpdateDto.Name;
        resource.Url = resourceUpdateDto.Url;
        resource.Type = resourceUpdateDto.Type;
    }
}