using Catalog.API.Entities;
using Catalog.API.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _repository;

    public CatalogController(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _repository.GetProduct(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [Route("[action]/{category}", Name = "GetProductByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductByCategory(string category)
    {
        var products = await _repository.GetProductByCategory(category);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        await _repository.CreateProduct(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        return Ok(await _repository.UpdateProduct(product));
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductById(string id)
    {
        return Ok(await _repository.DeleteProduct(id));
    }

}
//IACtionresult result farkı 