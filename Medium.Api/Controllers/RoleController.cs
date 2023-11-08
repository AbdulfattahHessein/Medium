using Medium.Api.Bases;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Interfaces.Services;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Api.Controllers
{
    public class RoleController : AppControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountsService _accountsService;
        private readonly IRoleServices _roleServices;

        public RoleController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IAccountsService accountsService, IRoleServices roleServices)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this._accountsService = accountsService;
            _roleServices = roleServices;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleRequest request)
        {
            var result = await _roleServices.CreateRoleAsync(request);

            return Ok(result);
        }

        [HttpGet("{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var result = await _roleServices.GetRoleByNameAsync(new GetRoleRequest(name));

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleServices.GetAllRolesAsync();

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromQuery] UpdateRoleRequest request)
        {
            var result = await _roleServices.UpdateRoleAsync(request);

            return Ok(result);
        }

        [HttpDelete("{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            var result = await _roleServices.DeleteRoleAsync(new DeleteRoleRequest(name));

            return Ok(result);
        }
    }
}


