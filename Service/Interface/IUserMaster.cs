using Microsoft.AspNetCore.Mvc;

namespace minio.Service.Interface
{
    public interface IUserMaster
    {
        Task<ActionResult> Login(string email, string password);
    }
}
