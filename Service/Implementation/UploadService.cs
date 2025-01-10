using Azure.Core;
using minio.Model.DB;
using minio.Model.DTO;
using minio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace minio.Service.Implementation
{
    public class UploadService : IUploadService
    { 
        private readonly Context _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
 
        public UploadService(Context context, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult> UploadBase64File(FileUploadBase64DTO uploadDTO)
        {
            if (string.IsNullOrEmpty(uploadDTO.base64Image))        
                return new BadRequestObjectResult("No Base64 image data provided.");

            if (string.IsNullOrEmpty(uploadDTO.UserId.ToString()))
                return new BadRequestObjectResult("User is required.");

            try
            {
                // Base path for storing files
                var basePath = Path.Combine(_environment.WebRootPath, "Uploads");

                // Create a user folder if it doesn't exist
                var userFolder = Path.Combine(basePath, uploadDTO.UserId.ToString());
                if (!Directory.Exists(userFolder))
                    Directory.CreateDirectory(userFolder);

                // Generate unique file name with a desired extension (e.g., .jpg)
                var fileExtension = ".jpg"; // You can determine the extension based on the file content or allow the user to send it.
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(userFolder, fileName);

                // Decode the Base64 string into a byte array
                var imageBytes = Convert.FromBase64String(uploadDTO.base64Image);

                // Save the image to disk
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                // Generate the full file URL (including domain)
                var fileUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Uploads/{uploadDTO.UserId}/{fileName}";

                // Save file information to the database
                var fileInfo = new minio.Model.DB.FileInfo
                {
                    URL = fileUrl,
                    UploadDate = DateTime.UtcNow,
                    UserId = uploadDTO.UserId
                };

                _context.FileInfo.Add(fileInfo);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new { Message = "File uploaded successfully", FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Internal server error: " + ex.Message);
            }
        }

        public async Task<ActionResult> UploadFile(FileUploadDTO uploadDTO)
        {
            if (uploadDTO.file == null || uploadDTO.file.Length == 0)
                return new BadRequestObjectResult("No file was uploaded.");

            if (string.IsNullOrEmpty(uploadDTO.UserId.ToString()))
                return new BadRequestObjectResult("User is required.");

            try
            {
                // Base path for storing files
                var basePath = Path.Combine(_environment.WebRootPath, "Uploads");

                // Create a user folder if it doesn't exist
                var userFolder = Path.Combine(basePath, uploadDTO.UserId.ToString());
                if (!Directory.Exists(userFolder))
                    Directory.CreateDirectory(userFolder);

                // Generate unique file name with original extension
                var fileExtension = Path.GetExtension(uploadDTO.file.FileName);
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(userFolder, fileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadDTO.file.CopyToAsync(stream);
                }
                 
                // Generate the full file URL (including domain)
                var fileUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Uploads/{uploadDTO.UserId}/{fileName}";

                // Save file information to the database
                var fileInfo = new minio.Model.DB.FileInfo
                {
                    URL = fileUrl,
                    UploadDate = DateTime.UtcNow,
                    UserId = uploadDTO.UserId
                };

                _context.FileInfo.Add(fileInfo);
                await _context.SaveChangesAsync();

                return new OkObjectResult(new { Message = "File uploaded successfully", FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Internal server error: " + ex.Message);
            }
        }

    }
}
