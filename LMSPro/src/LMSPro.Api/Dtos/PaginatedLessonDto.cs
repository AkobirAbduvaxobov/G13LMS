namespace LMSPro.Api.Dtos
{
    public class PaginatedLessonDto
    {
        public required List<LessonGetDto> Data { get; set; }
        public int TotalCount { get; set; }
    }
}