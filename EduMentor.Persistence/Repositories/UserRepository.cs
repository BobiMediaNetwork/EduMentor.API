using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Enum;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using EduMentor.Persistence.Context;

namespace EduMentor.Persistence.Repositories;

public class UserRepository(EduMentorDbContext context) : IUserRepository
{
    public ResponseType<User> Add(User entity)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public ResponseType<Guid> GetAllStudentsRegisteredAtAClass(Guid classId)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> GetAllUsersByRole(RoleEnum role)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> GetObjectById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> GetUserByIdWithOwnProperties(Guid id)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> IsEmailAndUsernameUnique(string? email, string? username)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> IsUserACertainType(Guid id, RoleEnum role)
    {
        throw new NotImplementedException();
    }

    public ResponseType<User> Update(User entity)
    {
        throw new NotImplementedException();
    }
}
