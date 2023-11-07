using Medium.Api.Bases;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medium.Api.Controllers
{

    public class AccountsController : AppControllerBase
    {
        private readonly IEmailService emailService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountsService _accountsService;
        private readonly IRoleServices _roleServices;

        public AccountsController(IEmailService emailService, UserManager<ApplicationUser> userManager, IAccountsService accountsService, IRoleServices roleServices)
        {
            this.emailService = emailService;
            this.userManager = userManager;
            this._accountsService = accountsService;
            _roleServices = roleServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterRequest request)
        {

            var result = await _accountsService.Register(request);

            return ApiResult(result);

        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var response = await emailService.ConfirmEmail(request);
            return ApiResult(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var result = await _accountsService.Login(request);

            return ApiResult(result);

        }

        #region old code
        //[HttpPost("register")]
        //public async Task<IActionResult> Registration(RegisterUserDto userDto)
        //{
        //    ApplicationUser user = new()
        //    {
        //        UserName = userDto.UserName,
        //        Email = userDto.Email,
        //    };
        //    var result = await userManager.CreateAsync(user, userDto.Password);

        //    return result.Succeeded ? Ok(user) : BadRequest(result.Errors);

        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginUserDto userDto)
        //{
        //    var user = await userManager.FindByNameAsync(userDto.UserName);
        //    if (user != null)
        //    {
        //        var rightPassword = await userManager.CheckPasswordAsync(user, userDto.Password);
        //        if (rightPassword)
        //        {

        //            var token = await GenerateJwtTokenAsync(user);
        //            return Ok(new
        //            {
        //                token
        //            });
        //        }
        //    }
        //    return Unauthorized();
        //}
        //private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        //{
        //    //Token claims
        //    var claims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                new Claim(ClaimTypes.Name, user.UserName),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            };
        //    // Get user roles
        //    var roles = await userManager.GetRolesAsync(user);
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
        //    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    //create token
        //    JwtSecurityToken token = new JwtSecurityToken(
        //        issuer: configuration["JWT:ValidIssur"],
        //        audience: configuration["JWT:ValidAudience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: signingCredentials
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        #endregion

        [HttpPost("addRoleToUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleUserRequest request)
        {
            var result = await _roleServices.AddRoleToUser(request);

            return ApiResult(result);
        }


        [HttpPost("UpdateUserRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateRoleToUserRequest request)
        {
            var result = await _roleServices.UpdateUserRoles(request);

            return ApiResult(result);
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await userManager.Users.ToListAsync();

            return Ok(result);
        }

    }
}
