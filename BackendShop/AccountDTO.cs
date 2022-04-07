namespace ShopBackend;

public class AccountDTO
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}