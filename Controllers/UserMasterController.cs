using minio.Interface;
using minio.Model.DB;
using minio.Model.DTO;
using minio.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace minio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {

        private readonly IBaseService<minio.Model.DB.UserMaster, UserMasterDTO> _service;
        private readonly IUserMaster _userMaster;

        public UserMasterController(IBaseService<UserMaster, UserMasterDTO> service, IUserMaster userMaster)
        {
            _service = service;
            _userMaster = userMaster;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email, string password)
        {
            return await _userMaster.Login(email, password);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserMasterDTO request)
        { 
            request.UserId = Guid.NewGuid();
            return await _service.Create(request);
        }

    }
}
