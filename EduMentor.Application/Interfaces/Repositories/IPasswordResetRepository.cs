using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;

namespace EduMentor.Application.Interfaces.Repositories;

public interface IPasswordResetRepository : IRepository<PasswordReset>
{
    public ResponseType<PasswordReset> GetPasswordResetByIdWithOwnProperties(Guid id);
}