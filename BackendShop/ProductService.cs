using Models;
using ShopBackend.Repositories;

namespace ShopBackend;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> GetProductById(Guid id)
    {
        return await _productRepository.GetProductById(id);
    }
    
    public async Task<Product?> FindProductByName(string name)
    {
        return await _productRepository.FindProductByName(name);
    }
    
    public async Task AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
            
        await _productRepository.Add(product);
    }

    public async Task RemoveProduct(Product product)
    {
        Product? newProduct = await _productRepository.FindProductByName(product.Name);
        
        await _productRepository.Remove(newProduct);
    }
    
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }
}