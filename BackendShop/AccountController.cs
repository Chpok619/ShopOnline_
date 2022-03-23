using Microsoft.AspNetCore.Mvc;
using Models;

namespace ShopBackend;

[Route("account")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("registration_account")]
    public async Task Registration(Account account)
    {
        await _accountService.Add(account);
    }
}