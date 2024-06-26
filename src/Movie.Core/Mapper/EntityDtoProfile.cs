using AutoMapper;
using Movies.Core.Dto;
using Movies.Repository.Entities;


namespace Movies.Core.Mapper;
public class EntityDtoProfile : Profile
{
    public EntityDtoProfile()
    {
        CreateMap<MovieEntity, MovieDto>();

        CreateMap<CreateMovieDto, MovieEntity>();
        CreateMap<MovieEntity, CreateMovieDto>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
    }
}