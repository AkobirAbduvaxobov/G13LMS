using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class HomeworkMapper
{
    public static HomeworkGetDto ToGetDto(this Homework homework)
    {
        var res = new HomeworkGetDto
        {
            HomeworkId = homework.HomeworkId,
            Title = homework.Title,
            Description = homework.Description,
            LessonId = homework.LessonId
        };

        if (homework.Lesson != null)
        {
            res.LessonTitle = homework.Lesson.Title;
        }

        return res;
    }
}