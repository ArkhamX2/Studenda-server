using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Server.Security.Service;
using Studenda.Server.Data.Transfer.Security;

namespace Studenda.Core.Server.Security.Controller
{
    [Route("api/security/roles")]
    [ApiController]
    public class RolesController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService roleService = roleService;

        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> ListRoles()
        {
            return await roleService.GetRolesList();
        }
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RoleRequest role)
        {
            var result = await roleService.Post(role.rolename);
            if (result)
            {
                return Ok();
            }
            return BadRequest($"Role with name{role.rolename} is exists");
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IdentityRole> EditRole([FromBody] RoleRequest role)
        {
            return await roleService.EditRole( role.rolename);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] RoleRequest role)
        {
            var result = await roleService.DeleteRole(role.rolename);
            if (result)
            {
                return Ok();
            }
            return BadRequest($"Role with name:{role.rolename} is not exists");
        }


    }
}
