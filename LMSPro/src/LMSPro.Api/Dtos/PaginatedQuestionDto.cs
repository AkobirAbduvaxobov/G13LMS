namespace LMSPro.Api.Dtos;

public class PaginatedQuestionDto
{
    public List<QuestionGetDto> QuestionGetDtos { get; set; }
    public int TotalCount { get; set; }
}
