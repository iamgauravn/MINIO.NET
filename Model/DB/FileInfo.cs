using minio.Models.DB;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minio.Model.DB
{
    [Table("FileInfo")]
    public class FileInfo : AuditableEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string URL { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public DateTime UploadDate { get; set; }

        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid UserId { get; set; }
    }
}
