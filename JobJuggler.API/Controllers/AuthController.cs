using System.Security.Claims;
using JobJuggler.API.DTOs;
using JobJuggler.API.Services;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly DataContext _context;
    private readonly TokenService _tokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(UserManager<AppUser> userManager,
        TokenService tokenService,
        ILogger<AuthController> logger,
        DataContext context, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
        _context = context;
        _signInManager = signInManager;
    }


    [AllowAnonymous]
    [HttpPost("/register")]
    public async Task<ActionResult<AuthUserDTO>> Register(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
        {
            return BadRequest($"Username {registerDto.Username} is already taken");
        }

        if ((!await _context.Companies.AnyAsync(x => x.Id == registerDto.CompanyId)))
        {
            return BadRequest($"Company with id {registerDto.CompanyId} does not exist");
        }

        _logger.LogInformation("Creating new user with username: " +
                               "{registerUsername} and email: {registerEmail}", registerDto.Username,
            registerDto.Email);

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto?.Username,
            CompanyId = registerDto.CompanyId,
            CreatedById = 0,
            DateCreated = DateTime.UtcNow
        };

        // user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return CreateUserObject(user);
        }
        else
        {
            _logger.LogError("Failure creating new user with username: {registerUsername} and email {registerEmail}",
                registerDto.Username, registerDto.Email);
        }

        return BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDTO>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return BadRequest();
        }

        //var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        var res = await _signInManager.PasswordSignInAsync(user.UserName!, loginDto.Password, false, false);

        if (res.Succeeded)
        {
            return CreateUserObject(user);
        }

        return Unauthorized();
    }

    // public async Task<ActionResult> Logout()
    // {
    //     await _signInManager.SignOutAsync();
    //
    //     return NoContent();
    // }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AuthUserDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

        return CreateUserObject(user);
    }

    private async Task SetRefreshToken(AppUser user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();

        //user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }


    private AuthUserDTO CreateUserObject(AppUser user)
    {
        var (tokenString, expirationDate) = _tokenService.CreateToken(user);
        return new AuthUserDTO
        {
            DisplayName = user.DisplayName,
            Token = tokenString,
            Username = user.UserName,
            Expires = expirationDate
        };
    }
}