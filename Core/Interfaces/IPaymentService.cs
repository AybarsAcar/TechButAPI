using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IPaymentService
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="basketId"></param>
    /// <returns></returns>
    Task<CustomerBasket> CreateUpdatePaymentIntent(string basketId);
  }
}