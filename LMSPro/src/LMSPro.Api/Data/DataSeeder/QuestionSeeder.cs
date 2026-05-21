using LMSPro.Api.Data;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Data.DataSeeder;

public static class QuestionSeeder
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Questions.Any()) return;

        var questions = new List<Question>();

        var random = new Random();

        // Faqat 1 va 2 lesson uchun
        var lessonIds = new List<long> { 1, 2 };

        foreach (var lessonId in lessonIds)
        {
            for (int i = 1; i <= 100; i++)
            {
                var correctAnswerIndex = random.Next(1, 5);

                var answer = correctAnswerIndex switch
                {
                    1 => "A",
                    2 => "B",
                    3 => "C",
                    _ => "D"
                };

                questions.Add(new Question
                {
                    Text = $"Lesson {lessonId} uchun savol #{i} nima haqida?",

                    VariantA = $"Variant A #{i}",
                    VariantB = $"Variant B #{i}",
                    VariantC = $"Variant C #{i}",
                    VariantD = $"Variant D #{i}",

                    Answer = answer,

                    LessonId = lessonId
                });
            }
        }

        await context.Questions.AddRangeAsync(questions);
        await context.SaveChangesAsync();
    }
}