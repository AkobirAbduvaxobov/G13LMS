using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class ExamMapper
{
    public static ExamGetDto ToGetDto(this Exam exam)
    {
        var res = new ExamGetDto
        {
            ExamId = exam.ExamId,
            Title = exam.Title,
            PassingScorePercentage = exam.PassingScorePercentage,
            LessonId = exam.LessonId
        };

        if (exam.Lesson != null)
        {
            res.Lesson = new LessonGetDto
            {
                LessonId = exam.Lesson.LessonId,
                Title = exam.Lesson.Title,
                Content = exam.Lesson.Content
            };
        }

        return res;
    }

    public static Exam ToEntity(this ExamCreateDto examCreateDto)
    {
        return new Exam
        {
            Title = examCreateDto.Title,
            PassingScorePercentage = examCreateDto.PassingScorePercentage,
            LessonId = examCreateDto.LessonId
        };
    }

    public static void ToUpdateEntity(this ExamUpdateDto examUpdateDto, Exam exam)
    {
        exam.Title = examUpdateDto.Title;
        exam.PassingScorePercentage = examUpdateDto.PassingScorePercentage;
    }
}