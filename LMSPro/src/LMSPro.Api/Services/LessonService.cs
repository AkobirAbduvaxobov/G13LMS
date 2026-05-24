using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public class LessonService : ILessonService
{
    public Task<long> CreateAsync(LessonCreateDto lessonCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long lessonId)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedLessonDto> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task<LessonGetDto> GetByIdAsync(long lessonId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(long lessonId, LessonUpdateDto lessonUpdateDto)
    {
        throw new NotImplementedException();
    }

    private void ValidatePaginationParameters(ref int skip, ref int take)
    {
        var res = skip + take;
        while (true)
        {
            if (skip < 0) skip = 0;
            if (take > 20) take = 20;
            if (res > 20)
            {
                take = 20 - skip;
                res = skip + take;
            }
            else
            {
                break;
            }
        }
    }
}
