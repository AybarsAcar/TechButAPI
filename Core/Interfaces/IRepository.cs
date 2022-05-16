using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
  /// <summary>
  /// Only Base Entities can be passed as a parameter
  /// which means only Types that have their own table
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IRepository<T> where T : BaseEntity
  {
    /// <summary>
    /// returns an Entity by its primary key from a table
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetByIdAsync(int id);

    /// <summary>
    /// returns all Entities in the table
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyList<T>> ListAllAsync();

    /// <summary>
    /// Returns an entity with a specific specification passed in
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<T> GetWithSpecification(ISpecification<T> specification);

    /// <summary>
    /// Returns all the entities with specifications
    /// </summary>
    /// <param name="specification">Specifications could be includes / filter expressions</param>
    /// <returns></returns>
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);

    /// <summary>
    /// Counts the number of items that is returned within Data of the response
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<int> CountAsync(ISpecification<T> specification);
  }
}