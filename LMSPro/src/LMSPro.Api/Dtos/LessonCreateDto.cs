using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos
{
    public class LessonCreateDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Order { get; set; }

        public TimeSpan Duration { get; set; }

        public long CourseId { get; set; }
    }
}
