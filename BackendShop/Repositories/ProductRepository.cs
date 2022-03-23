using Microsoft.EntityFrameworkCore;
using Models;

namespace ShopBackend.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<Product> GetByName(Guid id)
    {
        return await _appDbContext.Products.Where(product => product.Id == id).FirstAsync();
    }

    public async Task<Product?> FindByName(string name)
    {
        return await _appDbContext.Products.FirstOrDefaultAsync(product => product.Name == name);
    }

    public async Task Add(Product product)
    {
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Remove(Product product)
    {
        _appDbContext.Remove(product);
        await _appDbContext.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _appDbContext.Products.ToListAsync();
    }
}