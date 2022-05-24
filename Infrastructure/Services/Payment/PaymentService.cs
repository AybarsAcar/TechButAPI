using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services.Payment
{
   public class PaymentService : IPaymentService
  {
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unit;
    private readonly IConfiguration _config;

    public PaymentService(IBasketRepository basketRepository, IUnitOfWork unit, IConfiguration config)
    {
      _basketRepository = basketRepository;
      _unit = unit;
      _config = config;
    }

    public async Task<CustomerBasket> CreateUpdatePaymentIntent(string basketId)
    {
      StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

      var basket = await _basketRepository.GetBasketAsync(basketId);
      var shippingPrice = 0m;

      // check for delivery method
      if (basket.DeliveryMethodId.HasValue)
      {
        var deliveryMethod = await _unit.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);

        shippingPrice = deliveryMethod.Price;
      }

      // check the basket items and confirm the price
      foreach (var item in basket.Items)
      {
        var productItem = await _unit.Repository<Product>().GetByIdAsync(item.Id);

        // set the price if the price from the client does not match
        if (item.Price != productItem.Price)
        {
          item.Price = productItem.Price;
        }
      }

      var service = new PaymentIntentService();
      
      if (string.IsNullOrEmpty(basket.PaymentIntentId))
      {
        // we are creating a new intent
        var options = new PaymentIntentCreateOptions
        {
          Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
          Currency = "aud",
          PaymentMethodTypes = new List<string> { "card" }
        };

        var intent = await service.CreateAsync(options);

        basket.PaymentIntentId = intent.Id;
        basket.ClientSecret = intent.ClientSecret;
      }
      else
      {
        // we are updating a payment intent
        var options = new PaymentIntentUpdateOptions
        {
          Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
        };

        await service.UpdateAsync(basket.PaymentIntentId, options);
      }

      // update the basket
      await _basketRepository.CreateUpdateBasketAsync(basket);

      return basket;
    }
  }
}