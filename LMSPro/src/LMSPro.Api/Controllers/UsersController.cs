using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService UserService;

    public UsersController(IUserService userService)
    {
        UserService = userService;
    }

    //[Authorize(Roles = "Teacher,Admin,SuperAdmin")]
    [HttpGet]
    public async Task<List<UserGetDto>> GetAll()
    {
        var users = await UserService.GetAllAsync();
        return users;
    }
}
