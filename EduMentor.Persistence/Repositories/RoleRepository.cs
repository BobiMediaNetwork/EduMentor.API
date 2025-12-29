using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Domain.Generic;
using EduMentor.Domain.Model;
using EduMentor.Persistence.Context;

namespace EduMentor.Persistence.Repositories;

public class RoleRepository(EduMentorDbContext context) : IRoleRepository
{
    public ResponseType<Role> Add(Role entity)
    {
        try
        {
            context.Roles.Add(entity);
            context.SaveChanges();

            return new ResponseType<Role>
            {
                Object = entity,
                Message = "Role was added successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Role> Delete(Guid id)
    {
        try
        {
            var entity = GetObjectById(id);
            if (entity is not { IsSuccess: true, Object: not null })
            {
                return new ResponseType<Role>
                {
                    Message = entity.Message,
                    IsSuccess = true
                };
            }

            entity.Object!.IsDeleted = true;
            context.Roles.Update(entity.Object!);
            context.SaveChanges();
            return new ResponseType<Role>
            {
                Message = "Role deleted successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Role> GetAll()
    {
        try
        {
            var roles = context.Roles;

            return new ResponseType<Role>
            {
                Object = null,
                Collection = roles.AsEnumerable(),
                Message = "Roles was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Role> GetObjectById(Guid id)
    {
        try
        {
            var getAllRoles = GetAll();
            if (getAllRoles is not { IsSuccess: true, Collection: not null })
            {
                return new ResponseType<Role>
                {
                    Message = getAllRoles.Message,
                    IsSuccess = false
                };
            }

            var role = getAllRoles.Collection.FirstOrDefault(u => u.Id == id);
            if (role == null)
            {
                return new ResponseType<Role>
                {
                    Message = "Role could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<Role>
            {
                Object = role,
                Message = "Role find successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Role> IsNameUnique(string name)
    {
        try
        {
            if (!string.IsNullOrEmpty(name))
            {
                var user = context.Roles.FirstOrDefault(u => u.Name == name);

                if (user != null)
                {
                    return new ResponseType<Role>
                    {
                        Message = "Name is already used",
                        IsSuccess = false
                    };
                }
            }

            return new ResponseType<Role>
            {
                Object = null,
                Message = "Name is unique!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Role> Update(Role entity)
    {
        try
        {
            context.Roles.Update(entity);
            context.SaveChanges();

            return new ResponseType<Role>
            {
                Object = entity,
                Message = "Role updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Role>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }
}
