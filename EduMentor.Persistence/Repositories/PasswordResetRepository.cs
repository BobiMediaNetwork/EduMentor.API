using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using EduMentor.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EduMentor.Persistence.Repositories;

public class PasswordResetRepository(EduMentorDbContext context) : IPasswordResetRepository
{
    public ResponseType<PasswordReset> GetObjectById(Guid id)
    {
        try
        {
            var getAllPasswordResets = GetAll();
            if (getAllPasswordResets is not { IsSuccess: true, Collection: not null })
            {
                return new ResponseType<PasswordReset>
                {
                    Message = getAllPasswordResets.Message,
                    IsSuccess = false
                };
            }

            var passwordReset = getAllPasswordResets.Collection.FirstOrDefault(u => u.Id == id);
            if (passwordReset == null)
            {
                return new ResponseType<PasswordReset>
                {
                    Message = "PasswordReset could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<PasswordReset>
            {
                Object = passwordReset,
                Message = "PasswordReset find successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<PasswordReset> GetAll()
    {
        try
        {
            var passwordResets = context.PasswordResets.Include(g => g.User);

            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = passwordResets.AsEnumerable(),
                Message = "PasswordResets was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<PasswordReset> Add(PasswordReset entity)
    {
        try
        {
            context.PasswordResets.Add(entity);
            context.SaveChanges();

            return new ResponseType<PasswordReset>
            {
                Object = entity,
                Message = "PasswordReset was added successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<PasswordReset> Update(PasswordReset entity)
    {
        try
        {
            context.PasswordResets.Update(entity);
            context.SaveChanges();

            return new ResponseType<PasswordReset>
            {
                Object = entity,
                Message = "PasswordReset updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<PasswordReset> Delete(Guid id)
    {
        try
        {
            var entity = GetPasswordResetByIdWithOwnProperties(id);
            if (entity is not { IsSuccess: true, Object: not null })
            {
                return new ResponseType<PasswordReset>
                {
                    Message = entity.Message,
                    IsSuccess = true
                };
            }

            entity.Object!.IsDeleted = true;
            context.PasswordResets.Update(entity.Object!);
            context.SaveChanges();
            return new ResponseType<PasswordReset>
            {
                Message = "PasswordReset deleted successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<PasswordReset> GetPasswordResetByIdWithOwnProperties(Guid id)
    {
        try
        {
            var passwordReset = context.PasswordResets.FirstOrDefault(u => u.Id == id);
            if (passwordReset == null)
            {
                return new ResponseType<PasswordReset>
                {
                    Message = "PasswordReset could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<PasswordReset>
            {
                Object = passwordReset,
                Message = "PasswordReset find successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<PasswordReset>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }
}