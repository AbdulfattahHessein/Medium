//using Medium.BL.Features.Accounts.Request;
//using Medium.BL.Interfaces.Services;
//using Medium.Core.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace Medium.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RoleController : ControllerBase
//    {

//        private readonly IConfiguration configuration;
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly IAccountsService _accountsService;
//        private readonly IRoleServices _roleServices;

//        public RoleController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IAccountsService accountsService, IRoleServices roleServices)
//        {
//            this.configuration = configuration;
//            this.userManager = userManager;
//            this._accountsService = accountsService;
//            _roleServices = roleServices;
//        }

//        [HttpPost("createRole")]
//        public async Task<IActionResult> CreateRole([FromBody] AddRoleRequest request)
//        {
//            var result = await _roleServices.CreateRoleAsync(request);

//            return Ok(result);
//        }

//        [HttpGet("GetRoleByName")]
//        public async Task<IActionResult> GetRoleByName([FromQuery] GetRoleRequest request)
//        {
//            var result = await _roleServices.GetRoleByNameAsync(request);
//            //if (result == null)
//            //{
//            //    return BadRequest("Name Not Found");
//            //}

//            return Ok(result);
//        }

//        [HttpGet("getAllRoles")]
//        public async Task<IActionResult> GetAllRoles()
//        {
//            var result = await _roleServices.GetAllRolesAsync();

//            return Ok(result);
//        }

//        [HttpPut("updateRole")]
//        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request)
//        {
//            var result = await _roleServices.UpdateRoleAsync(request);

//            return Ok(result);
//        }

//        [HttpDelete("deleteRole")]
//        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleRequest request)
//        {
//            var result = await _roleServices.DeleteRoleAsync(request);

//            return Ok(result);
//        }
//    }
//}


