namespace ShopBackend.Exceptions;

public class IncorrectPasswordException: Exception
{
    public IncorrectPasswordException(): base("Wrong password")
    { }
}