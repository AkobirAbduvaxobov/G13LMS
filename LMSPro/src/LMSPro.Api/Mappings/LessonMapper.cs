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
            Duration = lesson.Duration,
            CourseId = lesson.CourseId
        };

        return res;
    }

    public static Lesson ToEntity(this LessonCreateDto lessonCreateDto)
    {
        return new Lesson
        {
            Title = lessonCreateDto.Title,
            Content = lessonCreateDto.Content,
            Order = lessonCreateDto.Order,
            Duration = lessonCreateDto.Duration,
            CourseId = lessonCreateDto.CourseId
        };
    }

    public static void UpdateEntity(this LessonUpdateDto lessonUpdateDto, Lesson lesson)
    {
        lesson.Title = lessonUpdateDto.Title;
        lesson.Content = lessonUpdateDto.Content;
        lesson.Order = lessonUpdateDto.Order;
        lesson.Duration = lessonUpdateDto.Duration;
    }
}