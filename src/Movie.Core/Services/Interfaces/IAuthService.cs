using Movies.Core.Dto;

namespace Movies.Core.Services.Interfaces;

public interface IAuthService
{
    Task<UserDto> RegisterUser(RegisterDto dto);
}