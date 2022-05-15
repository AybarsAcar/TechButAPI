using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
  /// <summary>
  /// Base Specification wrapper
  /// created with a filter criteria function
  /// allows Includes statements to be added
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class BaseSpecification<T> : ISpecification<T>
  {
    /// <summary>
    /// No Criteria is required if there is no filtering on the query
    /// </summary>
    public BaseSpecification()
    {
    }

    /// <summary>
    /// Criteria is passed in when filtering required
    /// </summary>
    /// <param name="criteria"></param>
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
      Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    /// <summary>
    /// to add an include specification
    /// this adds an expression to our list of expressions for the includes Statements 
    /// </summary>
    /// <param name="expression">IQueryable Include Expression</param>
    protected void AddInclude(Expression<Func<T, object>> expression)
    {
      Includes.Add(expression);
    }
  }
}