using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies4All.Core;
using Movies4All.Core.Consts;
using Movies4All.Core.Dto;
using Movies4All.Core.Models;
using System.Security.Claims;
using bc = BCrypt.Net.BCrypt;

namespace Movies4All.App.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("GetAllAdmin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var users = await _unitOfWork.Users.GetAllAsync(u => u.Role == RoleConst.Admin);
            if (users == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Invalid User!!!" });

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _unitOfWork.Users.GetAllAsync(u => u.Role == RoleConst.User);
            if (users == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message="Invalid User!!!" });

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult Delete(int id)
        {
            var user= _unitOfWork.Users.GetById(id);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Invalid user!!!" });

            _unitOfWork.Users.Delete(user);
            _unitOfWork.Complete();
            return Ok(new Response { Status="Succes", Message="Delated successfully."});
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> RegistreUser(RegistreUserDto dto)
        {
            var user = _unitOfWork.Users.GetById(u => u.Email == dto.Email);
            if (user != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User is exist!!!" });

            dto.Password = bc.HashPassword(dto.Password);
            user = _mapper.Map<User>(dto);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
            return Ok(dto);
        }
        [HttpPut("ChangeRole")]
        public async Task<IActionResult> ChangeRole(int UserId=0, string Role = RoleConst.User)
        {
            var user=_unitOfWork.Users.GetById(UserId);
            if (user == null)
                return NotFound(new Response { Status = "Error : ", Message="Invalid user!!!" });
            user.Role = Role;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
            return Ok(new Response { Status = "Succes, ", Message="Role is changed succesfully."});
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword cp)
        {
            var user = _unitOfWork.Users.GetById(u => u.Email == cp.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new { Status="Error", Message="User does not exists!"});

            if (string.Compare(cp.NewPassword, cp.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and confirm password does not match!!!" });

            if (!bc.Verify(cp.CurrentPassword, user.Password))
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The Current Password does not match!!!" });

            user.Password = bc.HashPassword(cp.NewPassword);
            var result = _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
            return Ok(new Response { Status = "Success", Message = "Password successfully changed." });
        }

        //For admin Only
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.Role}");
        }
        private UserDto GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDto
                {
                    Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
