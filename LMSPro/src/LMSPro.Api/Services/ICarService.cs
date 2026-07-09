using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ICarService
{
    Task<long> AddAsync(CarCreateDto carCreateDto);
    Task DeleteAsync(long carId);
    Task<List<CarGetDto>> GetAllAsync();
}