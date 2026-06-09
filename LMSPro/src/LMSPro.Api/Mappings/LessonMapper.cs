using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings
{
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

        public static LessonGetDto ToGetDto(this Lesson entity)
        {
            return new LessonGetDto
            {
                LessonId = entity.LessonId,
                Title = entity.Title,
                Content = entity.Content,
                Order = entity.Order,
                Duration = entity.Duration,
                CourseId = entity.CourseId
            };
        }

        public static void UpdateEntity(this Lesson entity, LessonUpdateDto dto)
        {
            entity.Title = dto.Title;
            entity.Content = dto.Content;
            entity.Order = dto.Order;
            entity.Duration = dto.Duration;
            entity.CourseId = dto.CourseId;
        }

    }
}
