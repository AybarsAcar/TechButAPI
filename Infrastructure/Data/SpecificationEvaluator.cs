using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  /// <summary>
  /// Object responsible for evaluating the Specification object against an entity
  /// and modifies the IQueryable for that specific Repository Query
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
  {
    /// <summary>
    /// Evaluates the specification objects and modifies the IQueryable object input and return it
    /// </summary>
    /// <param name="inputQuery"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
      var query = inputQuery;

      // evaluate what's inside specification
      if (specification.Criteria != null)
      {
        query = query.Where(specification.Criteria);
      }

      // evaluate the includes statements
      // if an empty list no includes will be added
      query = specification.Includes.Aggregate(query,
        (currentEntity, includeExpression) => currentEntity.Include(includeExpression));

      return query;
    }
  }
}