using HMSBusinessLogic.Manager.AccountManager;
using HMSContracts.Email;
using HMSContracts.Model.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hospital_Management_System.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmail _emailSender;
        public AccountController(IAccountManager accountManager, IEmail emailSender)
        {
            _accountManager = accountManager;
            _emailSender = emailSender;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var token = await _accountManager.Login(loginModel);
            if (token is not null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserModel userModel)
        {
            await _accountManager.Register(userModel);
            return Created();
        }

        [HttpPost("SendingEmail")]
        public async Task<IActionResult> SendingEmail()
        {
            await _emailSender.SendEmailAsync("sagdallrahman3@gmail.com", "This is test", "I am nada");
            return Ok("SendSuccessfully");
        }
    }
}
