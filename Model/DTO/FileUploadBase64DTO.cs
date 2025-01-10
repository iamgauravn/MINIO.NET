namespace minio.Model.DTO
{
    public class FileUploadBase64DTO
    {
        public Guid UserId { get; set; }
        public string base64Image { get; set; }
    }
}
