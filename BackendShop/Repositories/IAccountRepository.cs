using Models;

namespace ShopBackend.Repositories;

public interface IAccountRepository
{
    Task Add(Account account);
    Task Remove(Account account);
    Task<Account> GetByEmail(string email);
    Task<bool> IsEmailRegistered(string email);
}