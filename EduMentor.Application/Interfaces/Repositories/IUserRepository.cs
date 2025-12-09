using EduMentor.Domain.Enum;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;

namespace EduMentor.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public ResponseType<User> GetUserByIdWithOwnProperties(Guid id);
    public ResponseType<User> IsEmailAndUsernameUnique(string? email, string? username);
    public ResponseType<User> GetUserByEmail(string email);
    public ResponseType<User> IsUserACertainType(Guid id, RoleEnum role);
    public ResponseType<Guid> GetAllStudentsRegisteredAtAClass(Guid classId);
    public ResponseType<User> GetAllUsersByRole(RoleEnum role);
}