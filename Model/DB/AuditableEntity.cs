namespace minio.Models.DB;

public class AuditableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime DateCreated { get; set; }   
    public DateTime DateInserted { get; set; }
    public DateTime? DateUpdated { get; set; }
    public int? UserIdCreated { get; set; }
    public int? UserIdUpdated { get; set; }

    public void SetAuditableProperties() 
    {
        IsDeleted = false;
        DateCreated = DateTime.UtcNow;
        DateInserted = DateTime.UtcNow;
        DateUpdated = null;
        UserIdCreated = 1;
        UserIdUpdated = null; 
    }

    public void UpdateAuditableProperties()
    {
        DateUpdated = DateTime.UtcNow;
        //UserIdUpdated = null;
    }
}
