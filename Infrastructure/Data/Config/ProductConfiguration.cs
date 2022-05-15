using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
  /// <summary>
  /// Database & EF Core configuration for the Product Table
  /// </summary>
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      // configure Id
      builder.Property(p => p.Id).IsRequired();
      
      builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
      
      builder.Property(p => p.Description).IsRequired().HasMaxLength(400);

      builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

      builder.Property(p => p.ImageUrl).IsRequired();
      
      // configure relationships
      builder
        .HasOne(b => b.ProductBrand)
        .WithMany()
        .HasForeignKey(p => p.ProductBrandId);
      
      builder
        .HasOne(b => b.ProductType)
        .WithMany()
        .HasForeignKey(p => p.ProductTypeId);
    }
  }
}