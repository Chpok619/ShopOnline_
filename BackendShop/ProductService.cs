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
        return await _productRepository.GetByName(id);
    }
    
    public async Task<Product?> FindProductByName(string name)
    {
        return await _productRepository.FindByName(name);
    }
    
    public async Task AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
            
        await _productRepository.Add(product);
    }

    public async Task RemoveProduct(Product product)
    {
        Product? newProduct = await _productRepository.FindByName(product.Name);

        if (newProduct != null)
        {
            await _productRepository.Remove(newProduct);
        }
        else
        {
            throw new Exception();
        }
    }
    
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }
}