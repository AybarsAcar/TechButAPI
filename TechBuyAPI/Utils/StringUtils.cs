using System.Linq;

namespace TechBuyAPI.Utils
{
  public static class StringUtils
  {
    /// <summary>
    /// String extension method that converts a camel case to snake case 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToSnakeCase(this string str)
    {
      return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
  }
}