using Catalog.Data;
using Catalog.Domain;

namespace Catalog.Services;

public interface IProductService
{
    Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, CancellationToken ct = default);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<PagedResult<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, CancellationToken ct = default)
    {
        // TODO: implement paging over _repository.GetAllAsync().
        throw new NotImplementedException();
    }
}
