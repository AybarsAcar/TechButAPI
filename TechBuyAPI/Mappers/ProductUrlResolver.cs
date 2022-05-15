using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using TechBuyAPI.DTOs;

namespace TechBuyAPI.Mappers
{
  /// <summary>
  /// TODO: Delete this resolver and Implement Cloud File Storage
  /// this resolver won't be required after we implement Cloudinary
  /// and start storing images in Cloudinary (maybe Amazon S3)
  /// </summary>
  public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
  {
    private readonly IConfiguration _config;

    public ProductUrlResolver(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
      if (!string.IsNullOrEmpty(source.ImageUrl))
      {
        return _config["ApiUrl"] + source.ImageUrl;
      }

      return null;
    }
  }
}