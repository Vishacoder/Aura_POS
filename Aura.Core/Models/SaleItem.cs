using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.Models;

public class SaleItem
{
    public int Id { get; set; }

    // Foreign Keys
    public int SaleId { get; set; }
    public int ProductId { get; set; }

    // Transaction Details
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Price at the time of sale
    public decimal Subtotal { get; set; }

    // Navigation properties (required for EF Core to link the entities)
    public Sale Sale { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
