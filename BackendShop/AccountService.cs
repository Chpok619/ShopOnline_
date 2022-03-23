using Models;
using ShopBackend.Repositories;

namespace ShopBackend;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Add(Account account)
    {
        account.Id = Guid.NewGuid();
        await _accountRepository.Add(account);
    }

    public async Task<bool> IsEmailRegistered(string email)
    {
        return await _accountRepository.IsEmailRegistered(email);
    }
}