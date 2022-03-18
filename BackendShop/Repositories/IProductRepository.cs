using Models;

namespace ShopBackend.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductById(Guid id);
    Task<Product?> FindProductByName(string name);
    Task Add(Product product);
    Task Remove(Product product);
    Task<IReadOnlyList<Product>> GetProducts();
}