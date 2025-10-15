using Aura.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Data.Seed;

public static class DataSeeder
{
    // This method will check if data exists and add it if the table is empty.
    public static async Task SeedAsync(AuraDbContext context)
    {
        // Check if any products exist. If so, exit.
        if (context.Products.Any())
        {
            return;
        }

        var products = new List<Product>
        {
            new Product { Name = "Gala Apple", SKU = "001001", Price = 0.75m, StockQuantity = 150 },
            new Product { Name = "Organic Banana", SKU = "001002", Price = 0.49m, StockQuantity = 200 },
            new Product { Name = "Whole Milk (Gallon)", SKU = "002001", Price = 3.99m, StockQuantity = 50 },
            new Product { Name = "Artisan Bread Loaf", SKU = "003001", Price = 5.50m, StockQuantity = 30 },
            new Product { Name = "Sparkling Water (Case)", SKU = "004001", Price = 12.99m, StockQuantity = 10 }, // Low stock item
            new Product { Name = "Energy Drink (Can)", SKU = "005001", Price = 2.00m, StockQuantity = 100 },
        };

        // Add the products to the context
        await context.Products.AddRangeAsync(products);

        // Commit the changes to the database
        await context.SaveChangesAsync();
    }
}