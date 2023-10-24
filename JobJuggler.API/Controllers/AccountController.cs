using JobJuggler.API.DTOs;
using JobJuggler.API.Services;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly DataContext _context;
    private readonly TokenService _tokenService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<AppUser> userManager, TokenService tokenService, ILogger<AccountController> logger, DataContext context) {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) {
            return BadRequest();
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result) {
            return CreateUserObject(user);
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username)) {
            return BadRequest($"Username {registerDto.Username} is already taken");
        }
        if ((!await _context.Companies.AnyAsync(x => x.Id == registerDto.CompanyId))) {
            return BadRequest($"Company with id {registerDto.CompanyId} does not exist");
        }
        _logger.LogInformation("Creating new user with username: " +
            "{registerUsername} and email: {registerEmail}", registerDto.Username, registerDto.Email);

        var user = new AppUser {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto?.Username,
            CompanyId = registerDto.CompanyId,
            CreatedById = 0,
            DateCreated = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded) {
            return CreateUserObject(user);
        } else {
            _logger.LogError("Failure creating new user with username: {registerUsername} and email {registerEmail}", registerDto.Username, registerDto.Email);
        }

        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser() {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);
    }

    private UserDto CreateUserObject(AppUser user) {
        return new UserDto {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName
        };
    }
}
