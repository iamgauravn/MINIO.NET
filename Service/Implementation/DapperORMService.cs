using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient; 
using minio.Interface;
using minio.Models.DB;
using Dapper;

namespace minio.Services.Implementation;

public class DapperORMService : ISQLORMService
{
    private readonly ILogger<DapperORMService> _logger;

    public DapperORMService(ILogger<DapperORMService> logger)
    {
        _logger = logger;
    }

    public async Task<T?> ExecuteQuery<T>(string query, object? parameters = null) where T : AuditableEntity
    {
        try
        {
            using IDbConnection db = new SqlConnection("Data Source=DESKTOP-BEAEQQ2;Integrated Security=true;Initial Catalog=minio;TrustServerCertificate=True;MultipleActiveResultSets=true;"); 
            return await db.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing query");
            return null;
        }
    }
}