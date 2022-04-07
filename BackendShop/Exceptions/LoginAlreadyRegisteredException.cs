namespace ShopBackend.Exceptions;

public class LoginAlreadyRegisteredException : Exception
{
    public LoginAlreadyRegisteredException(): base("Login already registered")
    { }
}