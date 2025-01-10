using minio.Interface;
using minio.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace minio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IBaseService<minio.Model.DB.FileInfo, FileInfoDTO> _service;
        private readonly IUploadService _uploadService;

        public UploadController(IBaseService<minio.Model.DB.FileInfo, FileInfoDTO> service, IUploadService uploadService)
        {
            _service = service;
            _uploadService = uploadService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SieveModel sieveModel)
        {
            return await _service.Retrieve(sieveModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _service.RetrieveByID(id);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] FileInfoDTO request)
        //{

        //    request.UploadDate = DateTime.Now;
        //    return await _service.Create(request);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FileInfoDTO request)
        {
            return await _service.Update(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.Delete(id);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDTO uploadDTO)
        {
            return await _uploadService.UploadFile(uploadDTO);
        }

        [HttpPost("upload/base64")]
        public async Task<ActionResult> UploadBase64Image([FromBody] FileUploadBase64DTO uploadDTO)
        {
            return await _uploadService.UploadBase64File(uploadDTO);
        }


    }
}
