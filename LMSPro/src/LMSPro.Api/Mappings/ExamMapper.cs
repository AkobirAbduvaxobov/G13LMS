using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class ExamMapper
{
    public static Exam ToEntity(this ExamCreateDto dto)
    {
        return new Exam
        {
            Title = dto.Title,
            PassingScorePercentage = dto.PassingScorePercentage,
            LessonId = dto.LessonId
        };
    }

    public static ExamGetDto ToGetDto(this Exam exam)
    {
        return new ExamGetDto
        {
            ExamId = exam.ExamId,
            Title = exam.Title,
            PassingScorePercentage = exam.PassingScorePercentage,
            LessonId = exam.LessonId
        };
    }

    public static void UpdateEntity(this ExamUpdateDto dto, Exam exam)
    {
        exam.Title = dto.Title;
        exam.PassingScorePercentage = dto.PassingScorePercentage;
    }
}
