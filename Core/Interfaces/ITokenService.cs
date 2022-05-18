using Core.Entities.Identity;

namespace Core.Interfaces
{
  /// <summary>
  /// Service class responsible for generating and validating JWT Token
  /// </summary>
  public interface ITokenService
  {
    /// <summary>
    /// Crates a JWT token for a given AppUser object
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string CreateToken(AppUser user);
  }
}