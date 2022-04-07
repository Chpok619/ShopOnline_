using System.Text;

namespace ShopBackend;

public class JWTKey
{
    public string SigningKey { get; set; } = "";
    public TimeSpan LifeTime { get; set; }
    public string Issuer { get; set; } = "";
    
    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}