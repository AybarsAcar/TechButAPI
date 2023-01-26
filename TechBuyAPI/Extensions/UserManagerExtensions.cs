using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TechBuyAPI.Extensions;

/// <summary>
/// Extensions for the UserManager in ASP.NET CORE Identity
/// </summary>
public static class UserManagerExtensions
{
  /// <summary>
  /// returns the user with email and address
  /// extension method to bring navigation property
  /// </summary>
  /// <param name="input"></param>
  /// <param name="user"></param>
  /// <returns></returns>
  public static async Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input,
    ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users
      .Include(u => u.Address)
      .SingleOrDefaultAsync(x => x.Email == email);
  }

  public static async Task<AppUser> FindByEmailFromClaimsPrincipleAsync(this UserManager<AppUser> input,
    ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users
      .SingleOrDefaultAsync(x => x.Email == email);
  }
}