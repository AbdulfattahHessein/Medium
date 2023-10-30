using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.Features.Accounts.Validators;
using Medium.BL.Interfaces.Services;
using Medium.BL.ResponseHandler;
using Medium.Core.Entities;
using Medium.Core.Interfaces.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Medium.BL.ResponseHandler.ApiResponseHandler;


namespace Medium.BL.AppServices
{
    public class AccountsService : AppService, IAccountsService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsService(IConfiguration configuration, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var rightPassword = await userManager.CheckPasswordAsync(user, request.Password);
                if (rightPassword)
                {
                    var token = await GenerateJwtTokenAsync(user);
                    var response = new LoginResponse(token);
                    return Success(response);
                }
            }
            return UnAuthorized<LoginResponse>();
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

            await UnitOfWork.Publishers.InsertAsync(new Publisher() { User = user });
            await UnitOfWork.CommitAsync();

            var response = new RegisterResponse(user.UserName, user.PasswordHash, user.Email);

            return Success(response);
        }
        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var publisher = await UnitOfWork.Publishers.GetFirstAsync(p => p.UserId == user.Id);
            //Token claims
            var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, publisher!.Id.ToString()),
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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
