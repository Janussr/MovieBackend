using AutoMapper;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Repository.Entities;
using Movies.Core.Services.Interfaces;

namespace Movies.Core.Services;

    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly MovieDbContext _context;

        public AuthService(IMapper mapper, MovieDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<UserDto> RegisterUser(RegisterDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;

        }
    }
