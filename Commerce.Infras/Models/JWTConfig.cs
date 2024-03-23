namespace Commerce.Infras.Models;

public class JWTConfig
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string TokenKey { get; set; }
    public int TokenExpiry { get; set; }
}
