namespace LMSPro.Api.Dtos
{
    public class PaginatedLessonDto
    {
        public List<LessonGetDto> Data { get; set; } = new();
        public int TotalCount { get; set; }
    }
}