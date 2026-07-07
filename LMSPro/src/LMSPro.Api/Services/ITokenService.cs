using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface ITokenService
{
    string GetToken(UserGetDto userGetDto);
}