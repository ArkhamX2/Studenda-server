using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Server.Security.Service;

namespace Studenda.Core.Server.Security.Controller
{
    [Route("api/roles")]
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
        public async Task<IActionResult> PostRole([FromBody] string RoleName)
        {
            var result = await roleService.Post(RoleName);
            if (result)
            {
                return Ok();
            }
            return BadRequest($"Role with name{RoleName} is exists");
        }
        [HttpPut]
        public async Task<IdentityRole> EditRole([FromBody] string id, string rolename)
        {
            return await roleService.EditRole(id, rolename);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] string id)
        {
            var result = await roleService.DeleteRole(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest($"Role with id:{id} is not exists");
        }


    }
}
