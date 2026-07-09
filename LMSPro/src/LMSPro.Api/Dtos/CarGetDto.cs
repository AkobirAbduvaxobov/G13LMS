using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class CarGetDto
{
    public long CarId { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }

    public long UserId { get; set; }
    public User? User { get; set; }
}
