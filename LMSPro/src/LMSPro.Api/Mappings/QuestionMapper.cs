using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Mappings;

public static class QuestionMapper
{
    public static QuestionGetDto ToGetDto(this Question question)
    {
        var res = new QuestionGetDto
        {
            QuestionId = question.QuestionId,
            Text = question.Text,
            VariantA = question.VariantA,
            VariantB = question.VariantB,
            VariantC = question.VariantC,
            VariantD = question.VariantD,
            Answer = question.Answer,
            LessonId = question.LessonId
        };

        if(question.Lesson != null)
        {
            res.LessonTitle = question.Lesson.Title;
            res.LessonContent = question.Lesson.Content;
        }

        return res;
    }
    public static Question ToEntity(this QuestionCreateDto questionCreateDto)
    {
        return new Question
        {
            Text = questionCreateDto.Text,
            VariantA = questionCreateDto.VariantA,
            VariantB = questionCreateDto.VariantB,
            VariantC = questionCreateDto.VariantC,
            VariantD = questionCreateDto.VariantD,
            Answer = questionCreateDto.Answer,
            LessonId = questionCreateDto.LessonId
        };
    }
    public static void UpdateEntity(this QuestionUpdateDto questionUpdateDto, Question question)
    {
        question.Text = questionUpdateDto.Text;
        question.VariantA = questionUpdateDto.VariantA;
        question.VariantB = questionUpdateDto.VariantB;
        question.VariantC = questionUpdateDto.VariantC;
        question.VariantD = questionUpdateDto.VariantD;
        question.Answer = questionUpdateDto.Answer;
        question.LessonId = questionUpdateDto.LessonId;
    }
}
