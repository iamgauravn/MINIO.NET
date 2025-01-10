
using minio.Models.DB;

namespace minio.Interface;

public interface IGenericRepository<T> where T : AuditableEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Insert(T obj);
    T Update(T obj);
    T Delete(T obj);
}
