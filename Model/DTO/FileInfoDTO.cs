namespace minio.Model.DTO
{
    public class FileInfoDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UserId { get; set; }
    }
}
