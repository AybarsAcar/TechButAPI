using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seed
{
  public class AppIdentityDbContextSeed
  {
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
      if (!userManager.Users.Any())
      {
        var user = new AppUser
        {
          DisplayName = "Aybars Acar",
          Email = "aybars@test.com",
          UserName = "aybars",
          Address = new Address
          {
            FirstName = "Aybars",
            LastName = "Acar",
            Street = "10 the Street",
            City = "Sydney",
            State = "NSW",
            PostCode = "2000"
          }
        };

        await userManager.CreateAsync(user, "Pa$$w0rd");
      }
    }
  }
}