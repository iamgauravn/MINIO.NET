namespace minio.Model.DTO
{
    public class FileUploadDTO
    {
        public IFormFile file { get; set; }
        public Guid UserId { get; set; }
    }
}
