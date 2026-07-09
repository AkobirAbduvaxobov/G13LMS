using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/cars")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarService CarService;

    public CarsController(ICarService carService)
    {
        CarService = carService;
    }

    [Authorize(Roles = "Student,Teacher,Admin,SuperAdmin")]
    [HttpGet]
    public async Task<List<CarGetDto>> GetAllCars()
    {
        var cars = await CarService.GetAllAsync();
        return cars;
    }

    [Authorize(Roles = "Student,Teacher,Admin,SuperAdmin")]
    [HttpPost]
    public async Task<long> CreateCar(CarCreateDto carCreateDto)
    {
        var carId = await CarService.AddAsync(carCreateDto);
        return carId;
    }

    [Authorize(Roles = "Student,Teacher,Admin,SuperAdmin")]
    [HttpDelete("{carId}")]
    public async Task DeleteCar(long carId)
    {
        await CarService.DeleteAsync(carId);
    }
}
