using Medium.Api.Bases;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Medium.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;

        public EmailsController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.emailService = emailService;
        }
        [HttpPost("/SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] EmailSendRequest request)
        {
            var response = await emailService.SendEmail(request);
            return ApiResult(response);

        }
        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {

            if (userId == null || code == null)
            {
                return BadRequest("Invalid Url");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid User");
            }
            else
                code = Encoding.UTF8.GetString(bytes: Convert.FromBase64String(code));
            var result = await userManager.ConfirmEmailAsync(user, code);
            var Message = result.Succeeded ? "Thank you for confirmation " : " Please Try again";
            return Ok(Message);
        }

        [HttpPost("SendResetPassword")]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordRequest request)
        {
            var response = await emailService.SendResetPasswordCode(request);
            return ApiResult(response);
        }

        [HttpPost("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordRequest request)
        {
            var response = await emailService.ConfirmResetPassword(request);
            return ApiResult(response);
            // return Ok(response);

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequest request)
        {
            var response = await emailService.ResetPassword(request);
            return ApiResult(response);
            // return Ok(response);

        }
    }
}