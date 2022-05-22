namespace Core.Entities.OrderAggregate
{
  /// <summary>
  /// OrderAggregate.Address is bound to the Order
  /// the Address that the order will be going to
  /// this will be owned by the order - this won't have its own table
  /// </summary>
  public class Address
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostCode { get; set; }

    public Address()
    {
    }

    public Address(string firstName, string lastName, string street, string city, string state, string postCode)
    {
      FirstName = firstName;
      LastName = lastName;
      Street = street;
      City = city;
      State = state;
      PostCode = postCode;
    }
  }
}