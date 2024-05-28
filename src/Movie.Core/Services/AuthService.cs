using AutoMapper;
using Microsoft.Extensions.Logging;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Repository.Entities;
using Movies.Core.Services.Interfaces;

namespace Movies.Core.Services;

    public class AuthService(IMapper mapper, MovieDbContext context, ILogger<AuthService> logger) : IAuthService
    {

    public async Task<UserDto> RegisterUser(RegisterDto dto)
        {
            var user = mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await context.AddAsync(user);
            await context.SaveChangesAsync();

            var userDto = mapper.Map<UserDto>(user);
            logger.LogInformation(2,"Log Info create user");
            logger.LogInformation(3,"Log Info create user");
            logger.LogInformation(4,"Log Info create user");
            logger.LogInformation(5,"Log Info create user");
            return userDto;

        }
    }
