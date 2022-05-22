using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
  /// <summary>
  /// Entity framework about the properties order owns
  /// and the configuration of the enum
  /// </summary>
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.OwnsOne(order => order.ShipToAddress, a => { a.WithOwner(); });

      builder.Property(order => order.Status)
        .HasConversion(
          orderStatus => orderStatus.ToString(),
          o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
        );

      // if we delete an order, we delete order items too
      // 1 to many relationship
      builder.HasMany(order => order.OrderItems)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}