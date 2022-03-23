namespace Models;

public interface ICartForShop
{
    void AddToCart(Product product);
    void RemoveFromCart(Product product);
    IReadOnlyList<Product> GetCart();
    void RemoveAllProducts();
}