using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    /// <summary>
    /// checks if there is an already backing hashtable exists if not creates it
    /// if the repository exists it returns, if not creates and then returns if
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    /// <summary>
    /// Returns the number of changes to our database
    /// returns 0 if no changes are saved
    /// </summary>
    /// <returns></returns>
    Task<int> Complete();
  }
}