namespace LMSPro.Api.Dtos;

public class ResourceCreateDto
{
    public string Name { get; set; }

    public string Url { get; set; }

    public string Type { get; set; }

    public long LessonId { get; set; }
}