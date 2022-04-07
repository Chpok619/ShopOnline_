using Models;

namespace BlazorShop;

public class CartForShop: ICartForShop
{
    private List<Product> _products = new List<Product>();
    
    public void  AddToCart(Product product)
    {
        _products.Add(product);
    }

    public void RemoveFromCart(Product product)
    {
        _products.Remove(product);
    }

    public IReadOnlyList<Product> GetCart()
    {
        return _products;
    }

    public void RemoveAllProducts()
    {
        _products = new List<Product>();
    }
}