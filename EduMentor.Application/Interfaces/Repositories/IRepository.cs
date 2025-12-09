using EduMentor.Domain.Generic;

namespace EduMentor.Application.Interfaces.Repositories;

public interface IRepository<T>
{
    public ResponseType<T> GetObjectById(Guid id);
    public ResponseType<T> GetAll();
    public ResponseType<T> Add(T entity);
    public ResponseType<T> Update(T entity);
    public ResponseType<T> Delete(Guid id);
}
