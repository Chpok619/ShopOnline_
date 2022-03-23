using Microsoft.EntityFrameworkCore;
using Models;

namespace ShopBackend.Repositories;

public class AccountRepository: IAccountRepository
{
    private readonly AppDbContext _appDbContext;

    public AccountRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Account account)
    {
        await _appDbContext.Accounts.AddAsync(account);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Remove(Account account)
    {
        _appDbContext.Accounts.Remove(account);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Account> GetByEmail(string email)
    {
        return await _appDbContext.Accounts.FirstAsync(it => it.Email == email);
    }

    public async Task<bool> IsEmailRegistered(string email)
    {
        return await _appDbContext.Accounts.AnyAsync(it => it.Email == email);
    }
}