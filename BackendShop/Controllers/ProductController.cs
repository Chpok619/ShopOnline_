using Microsoft.AspNetCore.Mvc;
using Models;

namespace ShopBackend;

[Route("catalog")]
[ApiController]
public class ProductController: ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("get_products")]
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _productService.GetProducts();
    }

    [HttpPost("add_product")]
    public async Task AddProduct(Product product)
    {
        await _productService.AddProduct(product);
    }
    
    [HttpPost("remove_product")]
    public async Task RemoveProduct(Product product)
    {
        await _productService.RemoveProduct(product);
    }
}