using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.Models;

    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }

    public partial class Product : ObservableObject
    {
        public Product Clone()
        {
        return new Product
        {
            Id = this.Id,
            Name = this.Name,
            SKU = this.SKU,
            Price = this.Price,
            StockQuantity = this.StockQuantity,
        };
        }
    }


