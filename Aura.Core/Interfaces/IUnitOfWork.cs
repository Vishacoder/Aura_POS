using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Aura.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Expose the specific repositories the application needs
    IProductRepository Products { get; }

    // Note: We'll add ISaleRepository later
    // ISaleRepository Sales { get; } 

    // Commit method to save all staged changes (Add, Update, Delete) to the database
    Task<int> CompleteAsync();
}
