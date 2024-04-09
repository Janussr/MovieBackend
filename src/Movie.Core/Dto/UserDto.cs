namespace Movies.Core.Dto;

public class UserDto
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

}