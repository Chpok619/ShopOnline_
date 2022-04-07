using System.Net.Http.Headers;
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

    public async Task<IReadOnlyList<Product>?> GetProducts() => 
        await _client.GetFromJsonAsync<IReadOnlyList<Product>>($"{_host}/catalog/get_products");

    public async Task<Product?> FindProductByName(string name) =>
        await _client.GetFromJsonAsync<Product>($"{_host}/catalog/get_product_by_name?name={name}");

    public async Task AddProduct(Product product) => 
        await _client.PostAsJsonAsync($"{_host}/catalog/add_product", product);

    public async Task RemoveProduct(Product product) => 
        await _client.PostAsJsonAsync($"{_host}/catalog/remove_product", product);

    public async Task Registration(Account account) =>
        await _client.PostAsJsonAsync($"{_host}/account/registration", account);

    public async Task<LoginResponse?> SignIn(string login, string password)
    {
        LoginRequest loginRequest = new LoginRequest { Login = login, Password = password };
        
        var message = await _client.PostAsJsonAsync($"{_host}/account/signin", loginRequest);
        var response = await message.Content.ReadFromJsonAsync<LoginResponse>();
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response!.Token);
        
        return response;
    }
}