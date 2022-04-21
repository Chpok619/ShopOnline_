using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBackend.Exceptions;

namespace ShopBackend.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionFilter(IHostEnvironment hostEnvironment) =>
        _hostEnvironment = hostEnvironment;

    public void OnException(ExceptionContext context)
    {
        var message = GetMessageFromException(context);
        if (message is not null)
        {
            context.Result = new ObjectResult(
                new
                {
                    Succeeded = false,
                    Message = message
                });
            context.ExceptionHandled = true;
        }
    }

    private static string? GetMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            AccountNotFoundException => "Account not found!",
            EmailAlreadyRegisteredException => "Email already registered!",
            LoginAlreadyRegisteredException => "Login already registered",
            IncorrectPasswordException => "Wrong password!",
            _ => null
        };
    }
}