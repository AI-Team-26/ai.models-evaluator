using Catalog.Data;
using Catalog.Domain;
using Catalog.Services;
using Xunit;

namespace Catalog.Tests;

public class FakeProductRepository : IProductRepository
{
    private readonly List<Product> _products;
    public FakeProductRepository(IEnumerable<Product> products) => _products = products.ToList();

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Product>>(_products);
}

public class PagedResultTests
{
    // ---- PagedResult.TotalPages ----

    [Theory]
    [InlineData(10, 3, 4)]   // 3 full pages + 1 leftover -> 4, not 3 (floor division bug)
    [InlineData(9, 3, 3)]    // exact division -> 3
    [InlineData(1, 3, 1)]    // fewer items than page size -> 1
    [InlineData(0, 3, 0)]    // no items -> 0
    public void TotalPages_UsesCeilingDivision(int totalCount, int pageSize, int expected)
    {
        var result = new PagedResult<Product> { TotalCount = totalCount, PageSize = pageSize };
        Assert.Equal(expected, result.TotalPages);
    }

    [Fact]
    public void TotalPages_DoesNotThrow_WhenPageSizeIsZero()
    {
        var result = new PagedResult<Product> { TotalCount = 10, PageSize = 0 };
        var ex = Record.Exception(() => result.TotalPages);
        Assert.Null(ex);
    }

    [Fact]
    public void TotalPages_DoesNotThrow_WhenPageSizeIsNegative()
    {
        var result = new PagedResult<Product> { TotalCount = 10, PageSize = -5 };
        var ex = Record.Exception(() => result.TotalPages);
        Assert.Null(ex);
    }

    // ---- ProductService.GetPagedProductsAsync ----

    private static List<Product> MakeProducts(int count) =>
        Enumerable.Range(1, count)
            .Select(i => new Product { Id = i, Name = $"Product {i}", Price = i })
            .ToList();

    [Fact]
    public async Task FirstPage_ReturnsRequestedNumberOfItems()
    {
        var service = new ProductService(new FakeProductRepository(MakeProducts(25)));
        var page = await service.GetPagedProductsAsync(1, 10);
        Assert.Equal(10, page.Items.Count);
        Assert.Equal(25, page.TotalCount);
    }

    [Fact]
    public async Task LastPartialPage_ReturnsRemainderNotEmpty()
    {
        var service = new ProductService(new FakeProductRepository(MakeProducts(25)));
        var page = await service.GetPagedProductsAsync(3, 10); // items 21-25
        Assert.Equal(5, page.Items.Count);
    }

    [Fact]
    public async Task PageBeyondAvailableData_ReturnsEmptyItems_NotException()
    {
        var service = new ProductService(new FakeProductRepository(MakeProducts(10)));
        var page = await service.GetPagedProductsAsync(99, 10);
        Assert.Empty(page.Items);
        Assert.Equal(10, page.TotalCount); // total still reflects full dataset
    }

    [Fact]
    public async Task EmptyRepository_ReturnsEmptyResult_NotNull()
    {
        var service = new ProductService(new FakeProductRepository(Array.Empty<Product>()));
        var page = await service.GetPagedProductsAsync(1, 10);
        Assert.NotNull(page);
        Assert.NotNull(page.Items);
        Assert.Empty(page.Items);
    }

    [Fact]
    public async Task InvalidPageNumber_DoesNotThrowUnhandledException()
    {
        var service = new ProductService(new FakeProductRepository(MakeProducts(10)));
        var ex1 = await Record.ExceptionAsync(() => service.GetPagedProductsAsync(0, 10));
        var ex2 = await Record.ExceptionAsync(() => service.GetPagedProductsAsync(-1, 10));
        Assert.Null(ex1);
        Assert.Null(ex2);
    }

    [Fact]
    public async Task InvalidPageSize_DoesNotThrowUnhandledException()
    {
        var service = new ProductService(new FakeProductRepository(MakeProducts(10)));
        var ex1 = await Record.ExceptionAsync(() => service.GetPagedProductsAsync(1, 0));
        var ex2 = await Record.ExceptionAsync(() => service.GetPagedProductsAsync(1, -5));
        Assert.Null(ex1);
        Assert.Null(ex2);
    }

    [Fact]
    public async Task ExcessivePageSize_IsBoundedSomehow()
    {
        // A naive "solution" happily requests pageSize = 1_000_000 worth of
        // data. A thoughtful implementation caps it or rejects it instead of
        // silently trying to materialize everything.
        var service = new ProductService(new FakeProductRepository(MakeProducts(50)));
        var page = await service.GetPagedProductsAsync(1, 1_000_000);
        Assert.True(page.Items.Count <= 50, "Should not silently misbehave on an absurd page size.");
    }

    [Fact]
    public async Task Ordering_IsDeterministicAcrossRepeatedCalls()
    {
        // Skip/Take over a source with no explicit sort can return a
        // different order (or duplicate/skip items) between calls. Paging
        // must be over an explicitly, stably ordered sequence.
        var service = new ProductService(new FakeProductRepository(MakeProducts(30)));
        var first = await service.GetPagedProductsAsync(2, 10);
        var second = await service.GetPagedProductsAsync(2, 10);
        Assert.Equal(first.Items.Select(p => p.Id), second.Items.Select(p => p.Id));
    }
}
