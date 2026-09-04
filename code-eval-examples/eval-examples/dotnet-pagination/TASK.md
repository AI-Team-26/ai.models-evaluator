# Task: Paged product listing

We need a paged `GET /api/products` endpoint.

Files to change:
- `PagedResult.cs` — implement `TotalPages`.
- `ProductService.cs` — implement `GetPagedProductsAsync`.
- `ProductsController.cs` — wire the `GetProducts` action up to the service.

Requirements:
- `pageNumber` is 1-based.
- The endpoint should return items, the total item count, and the total page count.
- `IProductRepository.GetAllAsync` is the only way to reach the data; do not change its signature.
- Handle query parameters a client might reasonably (or unreasonably) send.
  Don't just handle the happy path — think about what a malicious or buggy client could pass, and what the right behavior is.

Do not modify `IProductRepository.cs` or `Product.cs`.
