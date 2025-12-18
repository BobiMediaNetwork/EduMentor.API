namespace EduMentor.Application.Interfaces.Security;
public interface ITokenService
{
    string CreateToken(Guid userId);
}