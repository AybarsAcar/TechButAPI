using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
  public class BaseRepository<T> : IRepository<T> where T : BaseEntity
  {
    private readonly StoreContext _context;

    public BaseRepository(StoreContext context)
    {
      _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      // we can set a DB table from the type using Set<T>()
      return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
      return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetWithSpecification(ISpecification<T> specification)
    {
      return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
      return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
      return await ApplySpecification(specification).CountAsync();
    }

    /// <summary>
    /// Applies the evaluator
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
      return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }
  }
}