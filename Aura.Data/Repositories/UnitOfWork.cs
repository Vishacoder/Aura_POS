// Aura.Data/Repositories/UnitOfWork.cs

using Aura.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Aura.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuraDbContext _context;

    // Backing field for lazy loading (instantiated only when first requested)
    private IProductRepository? _productRepository;

    public UnitOfWork(AuraDbContext context)
    {
        _context = context;
    }

    // Public property to expose the Product Repository
    public IProductRepository Products =>
        _productRepository ??= new ProductRepository(_context);

    // This is the transaction commit: saves all changes staged across ALL repositories
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Ensures the DbContext connection is properly closed and resources are freed
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}