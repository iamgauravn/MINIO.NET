
using minio.Models.DB;

namespace minio.Interface;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IGenericRepository<T> Repository<T>() where T : AuditableEntity;
}