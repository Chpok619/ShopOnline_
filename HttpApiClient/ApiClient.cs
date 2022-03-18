using System.Net.Http.Json;
using Models;

namespace HttpApiClient;

public class ApiClient
{
    private readonly string _host;
    private readonly HttpClient _client;

    public ApiClient(string? host = null, HttpClient? client = null)
    {
        _host = host ?? "https://localhost:7088";
        _client = client ?? new HttpClient();
    }

    public Task<IReadOnlyList<Product>?> GetProducts() => _client.GetFromJsonAsync<IReadOnlyList<Product>>($"{_host}/catalog/get_products");

    public Task<Product?> GetProductById(Guid id) => _client.GetFromJsonAsync<Product>($"{_host}/catalog/get_product_by_id?id={id}");

    public Task AddProduct(Product product) => _client.PostAsJsonAsync($"{_host}/catalog/add_product", product);

    public Task RemoveProduct(Product product) => _client.PostAsJsonAsync($"{_host}/catalog/remove_product", product);
}