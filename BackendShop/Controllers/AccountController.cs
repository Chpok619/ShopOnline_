using Microsoft.AspNetCore.Mvc;
using Models;
using ShopBackend.Exceptions;

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

    [HttpPost("registration")]
    public async Task Registration(Account account)
    {
        await _accountService.Register(account);
    }

    [HttpPost("signin")]
    public async Task<ActionResult<LoginResponse>> SignIn(LoginRequest request)
    {
        try
        {
            var (account, token) = await _accountService.SignIn( request.Login, request.Password );
            
            var loginResponse = new LoginResponse() { Account = account, Token = token};
            
            return loginResponse;
        }
        catch (AccountNotFoundException)
        {
            return Unauthorized("Account not found");
        }
        catch (IncorrectPasswordException)
        {
            return Unauthorized("Incorrect password");
        }
    }
}