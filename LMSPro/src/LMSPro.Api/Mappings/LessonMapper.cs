using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class LessonMapper
{
    public static Lesson ToEntity(this LessonCreateDto dto)
    {
        return new Lesson
        {
            Title = dto.Title,
            Content = dto.Content,
            Order = dto.Order,
            Duration = dto.Duration,
            CourseId = dto.CourseId
        };
    }

    public static LessonGetDto ToGetDto(this Lesson lesson)
    {
        return new LessonGetDto
        {
            LessonId = lesson.LessonId,
            Title = lesson.Title,
            Content = lesson.Content,
            Order = lesson.Order,
            Duration = lesson.Duration,
            CourseId = lesson.CourseId
        };
    }

    public static void UpdateEntity(this LessonUpdateDto dto, Lesson lesson)
    {
        lesson.Title = dto.Title;
        lesson.Content = dto.Content;
        lesson.Order = dto.Order;
        lesson.Duration = dto.Duration;
    }
}