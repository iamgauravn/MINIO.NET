using minio.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace minio.Interface
{
    public interface IUploadService
    {
        Task<ActionResult> UploadFile(FileUploadDTO uploadDTO);
        Task<ActionResult> UploadBase64File(FileUploadBase64DTO uploadDTO);
    }
}
