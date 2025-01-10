using Microsoft.EntityFrameworkCore;

namespace minio.Model.DB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FileInfo> FileInfo { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }

    }
}
