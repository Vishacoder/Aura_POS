using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aura.Core.Models;

namespace Aura.Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    // Specific methods required for POS business logic
    Task<IEnumerable<Product>> GetProductsBelowStockAsync(int threshold);
    Task<Product?> GetBySkuAsync(string sku); // Used for cashier scanning
}
