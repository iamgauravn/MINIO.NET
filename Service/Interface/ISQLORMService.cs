using minio.Models.DB;

namespace minio.Interface;

public interface ISQLORMService
{
    Task<T?> ExecuteQuery<T>(string query, object? parameters = null) where T : AuditableEntity;
}