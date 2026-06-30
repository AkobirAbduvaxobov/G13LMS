namespace LMSPro.Api.Dtos;

public class PaginatedLessonDto
{
    public List<LessonGetDto> LessonGetDtos { get; set; }
    public int TotalCount { get; set; }
}
