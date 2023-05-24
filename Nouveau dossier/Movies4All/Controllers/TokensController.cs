using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies4All.Core;
using Movies4All.Core.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using bc = BCrypt.Net.BCrypt;
using Movies4All.Core.Consts;
using System.Security.Cryptography;

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TokensController(IConfiguration config,IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._configuration = config;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpPost("SignIn")]
        public IActionResult Post([FromBody] UserSignInDto dto)
        {
            
            if (dto!=null && dto.Email!=null && dto.PasswordHash!=null)
            {
                var user = _unitOfWork.Users.GetById(u=>u.Email==dto.Email);

                if (user != null && bc.Verify(dto.PasswordHash, user.PasswordHash))
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("Firstname",user.FirstName),
                        new Claim("Lastname",user.Lastname),
                        new Claim(ClaimTypes.Name,$"{user.Lastname} {user.FirstName}"),
                        new Claim("Email",user.Email),
                        new Claim(ClaimTypes.Role,user.Role)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { Status = "Success : ", Token = tokenHandler, Role = (user.Role == RoleConst.User ? RoleConst.User : RoleConst.Admin) });
                }
                else
                    return BadRequest("Invalid email or password!!");
            }else 
                return BadRequest();
        }

    }
}
