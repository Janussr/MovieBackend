using Microsoft.AspNetCore.Mvc;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthenticationController(IAuthService authService)
        {
            _authenticationService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterDto dto)
        {
            await _authenticationService.RegisterUser(dto);
            return Ok("Account successfully created");
        }

    }
}
