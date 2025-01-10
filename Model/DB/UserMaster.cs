using minio.Models.DB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minio.Model.DB
{
    [Table("UserMaster")]
    public class UserMaster : AuditableEntity
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Password { get; set; }

    }
}
