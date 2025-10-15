using JobSync.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace JobSync.Infrastucture.Security.Cryptography;
public class Bcrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);
        return passwordHash;
    }
}
