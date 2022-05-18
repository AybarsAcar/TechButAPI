using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechBuyAPI.DTOs.Account;
using TechBuyAPI.Errors;

namespace TechBuyAPI.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
      ITokenService tokenService)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _userManager.FindByEmailAsync(loginDto.Email);

      if (user == null)
      {
        return Unauthorized(new ApiResponse(401));
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded)
      {
        return Unauthorized(new ApiResponse(401));
      }

      return new UserDto
      {
        Email = user.Email,
        DisplayName = user.DisplayName,
        Token = _tokenService.CreateToken(user)
      };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      var user = new AppUser
      {
        DisplayName = registerDto.DisplayName,
        Email = registerDto.Email,
        UserName = registerDto.Username
      };

      var results = await _userManager.CreateAsync(user, registerDto.Password);

      if (!results.Succeeded)
      {
        return BadRequest(new ApiResponse(400));
      }

      return new UserDto
      {
        Email = user.Email,
        DisplayName = user.DisplayName,
        Token = _tokenService.CreateToken(user)
      };
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
      var email = User.FindFirstValue(ClaimTypes.Email);

      var user = await _userManager.FindByEmailAsync(email);

      return new UserDto
      {
        Email = user.Email,
        DisplayName = user.DisplayName,
        Token = _tokenService.CreateToken(user)
      };
    }

    /// <summary>
    /// for asynchronous validation from the client side
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("email-exists")]
    public async Task<bool> CheckEmailExistsAsync([FromQuery] string email)
    {
      return await _userManager.FindByEmailAsync(email) != null;
    }

    [HttpGet("address")]
    [Authorize]
    public async Task<ActionResult<Address>> GetUserAddress()
    {
      var email = User.FindFirstValue(ClaimTypes.Email);

      var user = await _userManager.FindByEmailAsync(email);

      return user.Address;
    }
  }
}