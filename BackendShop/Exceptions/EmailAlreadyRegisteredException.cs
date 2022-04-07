namespace ShopBackend.Exceptions;

public class EmailAlreadyRegisteredException : Exception
{
    public EmailAlreadyRegisteredException(): base("Email already registered")
    { }
}