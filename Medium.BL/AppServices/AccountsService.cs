using AutoMapper;
using FluentValidation;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.Features.Accounts.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Medium.BL.ResponseHandler.ApiResponseHandler;


namespace Medium.BL.AppServices
{
    public class AccountsService : AppService, IAccountsService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUrlHelper urlHelper;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountsService(IConfiguration configuration, IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUrlHelper urlHelper,
            IEmailService emailService,
             IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContext)

        {
            this.configuration = configuration;
            this.httpContext = httpContext;
            this.userManager = userManager;
            this.urlHelper = urlHelper;
            this.emailService = emailService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            await DoValidationAsync<LoginRequestValidator, LoginRequest>(request);

            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                //    if (await userManager.IsEmailConfirmedAsync(user))
                //    {
                var rightPassword = await userManager.CheckPasswordAsync(user, request.Password);
                if (rightPassword)
                {
                    var token = await GenerateJwtTokenAsync(user);
                    var response = new LoginResponse(token);
                    return Success(response);
                }
                //}
            }
            return UnAuthorized<LoginResponse>("Invalid Username or Password");
        }

        public async Task<ApiResponse<RegisterResponse>> Register(RegisterRequest request)
        {
            await DoValidationAsync<RegisterRequestValidator, RegisterRequest>(request, userManager);

            ApplicationUser user = new()
            {
                UserName = request.UserName,
                Email = request.Email,
            };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            await UnitOfWork.Publishers.InsertAsync(new Publisher() { User = user, Name = user.UserName, PhotoUrl = "/Defaults/default-profile.png" });

            await userManager.AddToRoleAsync(user, "User");

            //Send Confirm Email
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var requestAccessor = httpContextAccessor.HttpContext.Request;
            var Url = requestAccessor.Scheme + "://" + requestAccessor.Host + urlHelper.Action("ConfirmEmail", "Accounts", new { UserId = user.Id, Code = code });


            var message = $"To Confirm Email Click Link: <a href='{Url}'> اضغط هنا</a>";
            // message or body
            var Request = new EmailSendRequest(user.Email, message, "ConFirm Email");
            await emailService.SendEmail(Request);

            var response = new RegisterResponse(user.UserName, user.Email);

            return Success(response, "Account Created Successfully");
        }
        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            //Token claims
            var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
            // Get user roles
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //create token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssur"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
