namespace EduMentor.Application.Interfaces.Security;

public interface ISecurityService
{
    bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
}