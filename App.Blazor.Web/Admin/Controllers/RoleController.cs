using App.Business.Model;
using App.Business.Services;
using App.DbAccess.Entities.Identity;
using App.EFCore.DynamicLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Area("admin")]
    [Authorize]
    [Route("api/[area]/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleService _roleService;

        public RoleController(RoleManager<Role> roleManager, IRoleService roleService)
        {
            _roleManager = roleManager;
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<bool> AddRole(RoleModel model)
        {
            var role = new Role { Name = model.Name };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DelRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new BadHttpRequestException("角色不存在。");
            }
            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateRole(RoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                throw new BadHttpRequestException("角色不存在。");
            }
            role.Name = model.Name;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        [HttpGet("{id}")]
        public async Task<RoleModel> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new BadHttpRequestException("角色不存在。");
            }
            var result = new RoleModel { Id = role.Id, Name = role.Name };
            return result;
        }

        [HttpGet]
        public Task<PageResult<List<RoleModel>>> GetRoles([FromQuery] RoleQueryModel model)
        {
            return _roleService.GetRolesAsync(model);
        }
    }
}
