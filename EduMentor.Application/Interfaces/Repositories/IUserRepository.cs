using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;

namespace EduMentor.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public ResponseType<User> IsEmailAndUsernameUnique(string? email, string? username);
    public ResponseType<User> GetUserByIdWithOwnProperties(Guid id);
}