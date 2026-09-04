using Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        // TODO: call _service.GetPagedProductsAsync and return an appropriate
        // response, including for query parameters a client shouldn't send.
        throw new NotImplementedException();
    }
}
