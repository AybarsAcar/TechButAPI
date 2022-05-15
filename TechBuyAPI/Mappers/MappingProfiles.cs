using AutoMapper;
using Core.Entities;
using TechBuyAPI.DTOs;

namespace TechBuyAPI.Mappers
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Product, ProductToReturnDto>()
        .ForMember(
          destination => destination.ProductBrand,
          options => options.MapFrom(
            source => source.ProductBrand.Name
          )
        )
        .ForMember(
          destination => destination.ProductType,
          options => options.MapFrom(
            source => source.ProductType.Name
          )
        )
        .ForMember(
          destination => destination.ImageUrl,
          options => options.MapFrom<ProductUrlResolver>()
        );
    }
  }
}