using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aura.Core.Models;

namespace Aura.Core.Interfaces;

// T is the Entity type (e.g., Product, Sale)
public interface IGenericRepository<T> where T : class
{
    // READ Operations
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);

    // WRITE Operations (staged for Unit of Work)
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
