using EduMentor.Application.Interfaces.Security;
using System.Security.Cryptography;
using System.Text;

namespace EduMentor.Infrastructure.Security;

public class SecurityService : ISecurityService
{
    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}