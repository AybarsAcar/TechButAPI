using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
  /// <summary>
  /// Describes a specification
  /// specifications are passed down to Generic Repository as an object which
  /// changes the IQueryable from the database
  /// it is just a wrapper around a Generic Expression Function
  /// </summary>
  public interface ISpecification<T>
  {
    /// <summary>
    /// Describes the Where criteria for filtering IQueryable
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }
    
    /// <summary>
    /// Describes the Includes operations for the navigation properties
    /// Includes navigation (nested) properties for the IQueryable
    /// </summary>
    /// <returns></returns>
    List<Expression<Func<T, object>>> Includes { get; }
  }
}