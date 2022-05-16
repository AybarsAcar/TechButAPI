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

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    /// <summary>
    /// to add an include specification
    /// this adds an expression to our list of expressions for the includes Statements 
    /// </summary>
    /// <param name="expression">IQueryable Include Expression</param>
    protected void AddInclude(Expression<Func<T, object>> expression)
    {
      Includes.Add(expression);
    }

    /// <summary>
    /// Adds an expression adds sets the OrderBy property
    /// of the specification
    /// Setter method
    /// </summary>
    /// <param name="expression"></param>
    protected void AddOrderBy(Expression<Func<T, object>> expression)
    {
      OrderBy = expression;
    }

    /// <summary>
    /// Adds an expression adds sets the OrderByDescending property
    /// of the specification
    /// Setter method
    /// </summary>
    /// <param name="expression"></param>
    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
      OrderByDescending = expression;
    }

    /// <summary>
    /// Applies paging to the specification query
    /// </summary>
    /// <param name="skip">Number of Items to skip</param>
    /// <param name="take">Number of Items to take</param>
    protected void ApplyPaging(int skip, int take)
    {
      Skip = skip;
      Take = take;
      IsPagingEnabled = true;
    }
  }
}