using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class CarService : ICarService
{
    private readonly IBaseRepository<Car> CarRepository;
    private readonly ICurrentUserService CurrentUserService;
    public CarService(IBaseRepository<Car> carRepository, ICurrentUserService currentUserService)
    {
        CarRepository = carRepository;
        CurrentUserService = currentUserService;
    }

    public async Task<long> AddAsync(CarCreateDto carCreateDto)
    {
        var car = new Car()
        {
            Model = carCreateDto.Model,
            Brand = carCreateDto.Brand,
            UserId = CurrentUserService.UserId ?? 0
        };

        await CarRepository.AddAsync(car);
        await CarRepository.SaveChangesAsync();

        return car.CarId;
    }

    public async Task DeleteAsync(long carId)
    {
        var cars = CarRepository.GetAllQuery();
        var car = await cars.FirstOrDefaultAsync(c => c.CarId == carId);

        if (car == null)
        {
            throw new KeyNotFoundException($"Car with ID {carId} not found.");
        }

        if(car.UserId != CurrentUserService.UserId)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this car.");
        }

        CarRepository.Delete(car);
        await CarRepository.SaveChangesAsync();
    }

    public Task<List<CarGetDto>> GetAllAsync()
    {
        var carsDto = CarRepository.GetAllQuery()
            .Where(c => c.UserId == CurrentUserService.UserId)
            .Select(c => new CarGetDto
            {
                CarId = c.CarId,
                Model = c.Model,
                Brand = c.Brand
            })
            .ToListAsync();

        return carsDto;
    }
}
