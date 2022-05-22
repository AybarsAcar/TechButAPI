using System;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;
using TechBuyAPI.DTOs.Order;

namespace TechBuyAPI.Mappers
{
  /// <summary>
  /// TODO: to be deleted after Cloudinary is implemented
  /// </summary>
  public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
  {
    private readonly IConfiguration _config;

    public OrderItemUrlResolver(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
      if (!string.IsNullOrEmpty(source.ItemOrdered.ImageUrl))
      {
        return _config["ApiUrl"] + source.ItemOrdered.ImageUrl;
      }

      return null;
    }
  }
}