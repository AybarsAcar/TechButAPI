using System.ComponentModel.DataAnnotations;

namespace TechBuyAPI.DTOs.Account
{
  /// <summary>
  /// address to return with the user and the address we post
  /// </summary>
  public class AddressDto
  {
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Street { get; set; }

    [Required] public string City { get; set; }

    [Required] public string State { get; set; }

    [Required] public string PostCode { get; set; }
  }
}