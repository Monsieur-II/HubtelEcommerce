using Commerce.Application.DTOs.Auth;
using Commerce.Application.Services.Auth;
using Commerce.Application.Utils;
using Commerce.Infras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Commerce.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JWTConfig _jwtConfig;

    public AuthController(UserManager<AppUser> userManager, IOptions<JWTConfig> jwtConfig)
    {
        _userManager = userManager;
        _jwtConfig = jwtConfig.Value;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginModel? model)
    {
        if (model == null)
            return BadRequest(new Response { Message = "Invalid data request", status = "error",
                Success = false
            });

        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return BadRequest(new Response { Message = "Username or password is incorrect",
                    status = "error",
                    Success = false
                }
            );
        }

        user.LastLogin = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var token = await TokenService.GenerateTokenAsync(user, _jwtConfig);

        return Ok(new LoginResponse() { Username = model.Username, Token = token });
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterUserModel? model)
    {
        if (model == null || model.Password != model.ConfirmPassword ||
            await _userManager.FindByNameAsync(model.PhoneNumber) != null)
        {
            return BadRequest(new Response
                {
                    Message = model == null ? "Invalid data request"
                        : model.Password != model.ConfirmPassword ? "Passwords do not match"
                        : "PhoneNumber already exists",
                    status = "error",
                    Success = false
                }
            );
        }

        var user = new AppUser
        {
            UserName = model.PhoneNumber,
            PhoneNumber = model.PhoneNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateCreated = DateTime.UtcNow
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
            return Created("/", new Response { Message = "User created successfully", status = "success", Success = true });
        
        return BadRequest(new Response{ Message = "An error occurred while creating user", status = "error", Success = false });
    }
}
