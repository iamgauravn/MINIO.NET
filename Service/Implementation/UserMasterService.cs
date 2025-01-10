using minio.Model.DB;
using minio.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace minio.Service.Implementation
{
    public class UserMasterService : IUserMaster
    {

        Context _db;

        public UserMasterService(Context db)
        {
            _db = db;
        }

        public async Task<ActionResult> Login(string email, string password)
        {

            var _user = _db.UserMaster.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsDeleted == false);
            if (_user == null)
            {
                return new BadRequestObjectResult("UnAuthorized");
            }

            // JWT Token generation
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("sdf5s4f6sd54fsdfsdf"); // Use a secure key and store it safely.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Id.ToString()), // Use unique identifier like UserId
                    new Claim(ClaimTypes.Email, _user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token validity
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return the token to the client
            return new OkObjectResult(new
            {
                Token = tokenString,
                ExpiresIn = tokenDescriptor.Expires
            });

        }
    }
}
