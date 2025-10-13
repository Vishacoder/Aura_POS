using Aura.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Data.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AuraDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AuraDbContext context)
    {
        _context = context;
        // The Set<T>() method allows us to access the appropriate DbSet (e.g., Products, Sales) dynamically
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity); // Stage the addition
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity); // Stage the deletion
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // AsNoTracking is important for read-only operations to improve performance
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(T entity)
    {
        // Mark the entity as modified so EF Core knows to update it when SaveChanges is called
        _context.Entry(entity).State = EntityState.Modified;
    }
}