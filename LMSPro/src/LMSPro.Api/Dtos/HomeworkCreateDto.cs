using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class HomeworkCreateDto
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public Lesson Lesson { get; set; }
    
    public long LessonId { get; set; }
}