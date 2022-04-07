using Models;
using ShopBackend.Repositories;
using ShopBackend.UnitOfWork;

namespace ShopBackend;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Product> GetProductById(Guid id)
    {
        return await _unitOfWork.ProductRepository.GetByName(id);
    }
    
    public async Task<Product?> FindProductByName(string name)
    {
        return await _unitOfWork.ProductRepository.FindByName(name);
    }
    
    public async Task AddProduct(Product product)
    {
        product.Id = Guid.NewGuid();
            
        await _unitOfWork.ProductRepository.Add(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveProduct(Product product)
    {
        Product? newProduct = await _unitOfWork.ProductRepository.FindByName(product.Name);

        if (newProduct != null)
        {
            await _unitOfWork.ProductRepository.Remove(newProduct);
            await _unitOfWork.SaveChangesAsync();
        }
        else
        {
            throw new ProductDoesNotExistException();
        }
    }
    
    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        return await _unitOfWork.ProductRepository.GetProducts();
    }
}

public class ProductDoesNotExistException : Exception
{
    public ProductDoesNotExistException(): base("Product doesn't exist!")
    { }
}