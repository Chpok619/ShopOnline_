using Microsoft.AspNetCore.Identity;
using Models;
using ShopBackend.Exceptions;
using ShopBackend.Repositories;
using ShopBackend.UnitOfWork;

namespace ShopBackend;

public class AccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GenerateTokenService _generateTokenService;
    private readonly IPasswordHasher<AccountDTO> _hasher;

    public AccountService(IUnitOfWork unitOfWork, GenerateTokenService generateTokenService, IPasswordHasher<AccountDTO> hasher)
    {
        _unitOfWork = unitOfWork;
        _generateTokenService = generateTokenService;
        _hasher = hasher;
    }

    public async Task Register(Account account)
    {
        if (await _unitOfWork.AccountRepository.IsEmailRegistered(account.Email))
            throw new EmailAlreadyRegisteredException();

        if (await _unitOfWork.AccountRepository.IsLoginRegistered(account.Login))
            throw new LoginAlreadyRegisteredException();

        AccountDTO accountDto = new AccountDTO()
        {
            Id = Guid.NewGuid(),
            Login = account.Login,
            Email = account.Email
        };
        
        var hashedPassword = _hasher.HashPassword(accountDto, account.Password);
        accountDto.PasswordHash = hashedPassword;
        
        await _unitOfWork.AccountRepository.Add(accountDto);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<(Account account, string token)> SignIn(string login, string password)
    {
        bool isExist = await _unitOfWork.AccountRepository.IsLoginRegistered(login);

        if (isExist)
        {
            AccountDTO accountDto = await _unitOfWork.AccountRepository.GetByLogin(login);

            string token;

            var checkPassword = _hasher.VerifyHashedPassword(accountDto, accountDto.PasswordHash, password);
            
            if (checkPassword == PasswordVerificationResult.Success)
            {
                token = _generateTokenService.GenerateToken(accountDto);

                Account account = new Account()
                    { Login = accountDto.Login, Email = accountDto.Email, Password = accountDto.PasswordHash };
                
                return (account, token);
            }
            else
            {
                throw new IncorrectPasswordException();
            }
        }
        else
        {
            throw new AccountNotFoundException();
        }
    }

    public async Task Remove(Account account)
    {
        
    }

    public async Task<AccountDTO> GetByEmail(string email)
    {
        return await _unitOfWork.AccountRepository.GetByEmail(email);
    }

    public async Task<AccountDTO> GetByLogin(string login)
    {
        return await _unitOfWork.AccountRepository.GetByLogin(login);
    }

    public async Task<bool> IsEmailRegistered(string email)
    {
        return await _unitOfWork.AccountRepository.IsEmailRegistered(email);
    }

    public async Task<bool> IsLoginRegistered(string login)
    {
        return await _unitOfWork.AccountRepository.IsLoginRegistered(login);
    }
}