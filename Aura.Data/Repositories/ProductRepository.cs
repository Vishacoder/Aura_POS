using Aura.Core.Interfaces;
using Aura.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aura.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AuraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsBelowStockAsync(int threshold)
    {
        return await _dbSet
            .Where(p => p.StockQuantity < threshold)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet
            // Find the product that matches the SKU, often used for barcode scanning
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }
}