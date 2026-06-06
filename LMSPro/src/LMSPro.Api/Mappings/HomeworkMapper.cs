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
            res.LessonContent = homework.Lesson.Content;
        }

        return res;
    }

    public static Homework ToEntity(this HomeworkCreateDto homeworkCreateDto)
    {
        return new Homework
        {
            Title = homeworkCreateDto.Title,
            Description = homeworkCreateDto.Description,
            LessonId = homeworkCreateDto.LessonId
        };
    }

    public static void ToUpdateEntity(this HomeworkUpdateDto homeworkUpdateDto, Homework homework)
    {
        homework.Title = homeworkUpdateDto.Title;
        homework.Description = homeworkUpdateDto.Description;

        
    }
}