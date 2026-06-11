using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class LessonMapper
{
    public static LessonGetDto ToGetDto(this Lesson lesson)
    {
        var res = new LessonGetDto
        {
            LessonId = lesson.LessonId,
            Title = lesson.Title,
            Content = lesson.Content,
            Order = lesson.Order,
            CourseId = lesson.CourseId,
            Duration = lesson.Duration
        };
        if (lesson.Questions != null)
        {
            res.Questions = lesson.Questions.Select(q => q.ToGetDto()).ToList();
        }
        if (lesson.Homeworks != null)
        {
            res.Homeworks = lesson.Homeworks.Select(h => h.ToGetDto()).ToList();
        }
        //if (lesson.Resources != null)
        //{
        //    res.Resources = lesson.Resources.Select(r => r.ToGetDto()).ToList();
        //}
        if (lesson.Exams != null)
        {
            res.Exams = lesson.Exams.Select(e => e.ToGetDto()).ToList();
        }
        return res;
    }

    public static Lesson ToEntity(this LessonCreateDto lessonDto)
    {
        var res = new Lesson
        {
            Title = lessonDto.Title,
            Content = lessonDto.Content,
            Order = lessonDto.Order,
            CourseId = lessonDto.CourseId,
            Duration = lessonDto.Duration
        };

        return res;
    }
    public static void ToUpdateEntity(this LessonUpdateDto lessonUpdateDto, Lesson lesson)
    {
        lesson.Title = lessonUpdateDto.Title;
        lesson.Content = lessonUpdateDto.Content;
        lesson.Order = lessonUpdateDto.Order;
        lesson.Duration = lessonUpdateDto.Duration;

    }
}
