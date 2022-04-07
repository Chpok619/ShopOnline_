using Models;

namespace ShopBackend.Repositories;

public interface IAccountRepository
{
    Task Add(AccountDTO account);
    Task Delete(AccountDTO account);
    Task<AccountDTO> GetByEmail(string email);
    Task<AccountDTO> GetByLogin(string login);
    Task<bool> IsEmailRegistered(string email);
    Task<bool> IsLoginRegistered(string login);
}