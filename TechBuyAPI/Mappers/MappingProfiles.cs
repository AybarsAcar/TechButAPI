using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using TechBuyAPI.DTOs;
using TechBuyAPI.DTOs.Account;
using TechBuyAPI.DTOs.Basket;
using TechBuyAPI.DTOs.Order;
using Address = Core.Entities.Identity.Address;

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

      CreateMap<Core.Entities.Identity.Address, AddressDto>();
      CreateMap<AddressDto, Address>();

      CreateMap<CustomerBasketDto, CustomerBasket>();
      CreateMap<BasketItemDto, BasketItem>();

      CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

      CreateMap<Order, OrderToReturnDto>()
        .ForMember(
          destination => destination.DeliveryMethod,
          options => options.MapFrom(
            source => source.DeliveryMethod.ShortName
          )
        )
        .ForMember(
          destination => destination.ShippingPrice,
          options => options.MapFrom(
            source => source.DeliveryMethod.Price
          )
        );

      CreateMap<OrderItem, OrderItemDto>()
        .ForMember(
          destination => destination.ProductId,
          options => options.MapFrom(
            source => source.ItemOrdered.ProductItemId
          )
        )
        .ForMember(
          destination => destination.ProductName,
          options => options.MapFrom(
            source => source.ItemOrdered.ProductName
          )
        )
        .ForMember(
          destination => destination.ImageUrl,
          options => options.MapFrom(
            source => source.ItemOrdered.ImageUrl
          )
        )
        .ForMember(
          destination => destination.ImageUrl,
          options => options.MapFrom<OrderItemUrlResolver>()
        );
    }
  }
}