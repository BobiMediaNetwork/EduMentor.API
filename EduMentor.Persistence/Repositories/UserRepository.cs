using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using EduMentor.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EduMentor.Persistence.Repositories;

public class UserRepository(EduMentorDbContext context) : IUserRepository
{
    public ResponseType<User> Add(User entity)
    {
        try
        {
            context.Users.Add(entity);
            context.SaveChanges();

            return new ResponseType<User>
            {
                Object = entity,
                Message = "User was added successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> Delete(Guid id)
    {
        try
        {
            var entity = GetUserByIdWithOwnProperties(id);
            if (entity is not { IsSuccess: true, Object: not null })
            {
                return new ResponseType<User>
                {
                    Message = entity.Message,
                    IsSuccess = true
                };
            }

            entity.Object!.IsDeleted = true;
            context.Users.Update(entity.Object!);
            context.SaveChanges();
            return new ResponseType<User>
            {
                Message = "User deleted successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> GetAll()
    {
        try
        {
            var users = context.Users
                .Include(u => u.Role);

            return new ResponseType<User>
            {
                Object = null,
                Collection = users.AsEnumerable(),
                Message = "Users was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> GetObjectById(Guid id)
    {
        try
        {
            var getAllUsers = GetAll();
            if (getAllUsers is not { IsSuccess: true, Collection: not null })
            {
                return new ResponseType<User>
                {
                    Message = getAllUsers.Message,
                    IsSuccess = false
                };
            }

            var user = getAllUsers.Collection.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new ResponseType<User>
                {
                    Message = "User could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<User>
            {
                Object = user,
                Message = "User find successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> GetUserByIdWithOwnProperties(Guid id)
    {
        try
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new ResponseType<User>
                {
                    Message = "User could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<User>
            {
                Object = user,
                Message = "User find successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> IsEmailAndUsernameUnique(string? email, string? username)
    {
        try
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    return new ResponseType<User>
                    {
                        Message = "Email is already used",
                        IsSuccess = false
                    };
                }
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    return new ResponseType<User>
                    {
                        Message = "Username is already used",
                        IsSuccess = false
                    };
                }
            }

            return new ResponseType<User>
            {
                Object = null,
                Message = "Email and password is unique!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> Update(User entity)
    {
        try
        {
            context.Users.Update(entity);
            context.SaveChanges();

            return new ResponseType<User>
            {
                Object = entity,
                Message = "User updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }
}
