using System.Text.Json;

namespace TechBuyAPI.Utils;

/// <summary>
/// Snake Case Naming Policy that applies to all HTTP Response from the API
/// </summary>
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
  public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

  private SnakeCaseNamingPolicy()
  {
  }

  public override string ConvertName(string name)
  {
    return name.ToSnakeCase();
  }
}