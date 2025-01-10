using System.ComponentModel.DataAnnotations;

namespace minio.Model.DTO
{
    public class UserMasterDTO
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
