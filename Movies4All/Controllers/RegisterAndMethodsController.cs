using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using AutoMapper;
using Movies4All.Core;
using bc = BCrypt.Net.BCrypt;
using Movies4All.Core.Dto;
using Movies4All.Core.Models;

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAndMethodsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterAndMethodsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> RegistreUser(RegistreUserDto dto)
        {
            var user = _unitOfWork.Users.GetById(u => u.Email == dto.Email);
            if (user != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User is exist!!!" });
            string verificationToken = GenerateVerificationToken();
            dto.Password = bc.HashPassword(dto.Password);
            //user.VirefieAT = null;
            //  user.VerificationToken = verificationToken;
            user = _mapper.Map<User>(dto);
            user.VerificationToken = verificationToken;
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
            SendVerificationEmail(user.Email, verificationToken);
            return Ok("User registered successfully. Please check your email for verification.");
        }
        [HttpGet("VerifyAcount")]
        public async Task<IActionResult> VerifyAount(string token)
        {
            var user = _unitOfWork.Users.GetById(p=>p.VerificationToken==token);
            if (user == null)
            {
                return BadRequest("Invalid verification token.");
            }
            user.VirefieAT = DateTime.Now;
            user.VerificationToken = null; // Remove the verification token
            _unitOfWork.Complete();
            return Ok("Email verified successfully.");
        }
     
        private string GenerateVerificationToken()
        {
            // Generate a unique verification token (e.g., using Guid)
            return Guid.NewGuid().ToString();
        }
        private void SendVerificationEmail(string userEmail, string verificationToken)
        {
            string fromEmail = "rharbuomar@gmail.com"; // Replace with your Gmail address
            string fromPassword = "twoqpowszzrggbqn"; // Replace with your Gmail password

            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(fromEmail);
            mail.To.Add(userEmail);
            mail.Subject = "Email Verification";
           if(GenerateVerificationLink(verificationToken)!=null)
            {
                mail.Body = $"Please click the following link to verify your email: " +
                $"{GenerateVerificationLink(verificationToken)}";
            }
            if (RestPassword(verificationToken) != null)
            {
                mail.Body = $"Please click the following link to Rest Password: " +
              $"{RestPassword(verificationToken)}";
            }
           

            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential(fromEmail, fromPassword);
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }
        private string GenerateVerificationLink(string token)
        {
            // Generate a verification link using the token
            string baseUrl = "https://192.168.1.14:5020/api/RegisterAndMethods"; // Replace with your website URL
            return $"{baseUrl}/VerifyAcount?token={token}"; // Modify the URL structure as per your requirements
        }
        private string RestPassword(string token)
        {
            // Generate a verification link using the token
            string baseUrl = "http://localhost:5500/ForgetPassword.html"; // Replace with your website URL
            return $"{baseUrl}?token={token}"; // Modify the URL structure as per your requirements
        }

        [HttpPost("Forgot Password")]
        public async Task<IActionResult> ForGotPassword(string email)
        {
            var user = _unitOfWork.Users.GetById(p => p.Email == email);
            if (user == null) { return BadRequest("Invalid Acount"); }
            var passwordToken = GenerateVerificationToken();
            user.RestPassword = passwordToken;
            user.RestTokenExpires = DateTime.Now.AddDays(1);
            _unitOfWork.Complete();
            SendVerificationEmail(user.Email,passwordToken);
            return Ok("Verify Email");
        }
        [HttpPost("Rest_Password")]
        public async Task<IActionResult> RestPassword(RestPassword password)
        {
           // var PasswordTokenRest = RestPassword(password.token);
            var token = _unitOfWork.Users.GetById(p=>p.RestPassword== password.token);
            if (token == null || token.RestTokenExpires < DateTime.Now) return BadRequest("Invalid Token");
            token.Password = bc.HashPassword(password.Password);  
            token.RestPassword = null;
            token.RestPassword = null;
            _unitOfWork.Complete();
            return Ok("Rest Password Seccessfuly");
            
        }
    }
}
