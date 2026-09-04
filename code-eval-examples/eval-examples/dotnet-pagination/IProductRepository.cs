using Catalog.Domain;

namespace Catalog.Data;

public interface IProductRepository
{
    // Returns all products currently in the store. In a real app this would
    // hit a database; here it's a stand-in so the paging logic can be
    // exercised and tested in isolation, without a real ordering guarantee.
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);
}
