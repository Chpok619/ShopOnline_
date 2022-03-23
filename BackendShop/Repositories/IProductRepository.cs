using Models;

namespace ShopBackend.Repositories;

public interface IProductRepository
{
    Task<Product> GetByName(Guid id);
    Task<Product?> FindByName(string name);
    Task Add(Product product);
    Task Remove(Product product);
    Task<IReadOnlyList<Product>> GetProducts();
}