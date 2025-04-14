using Microsoft.AspNetCore.Mvc;
using SecureAuthApi.Models;
using BudgetManagementSystemNew.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpPost]
       // [SwaggerOperation(Tags = new[] { "Role" })]
        public IActionResult AddRole([FromBody] Role role)
        {
            try
            {
                _roleRepository.AddRole(role);
                return Ok(new { Message = "Role added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the role.", Error = ex.Message });
            }
        }
        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Role" })]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = _roleRepository.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching roles.", Error = ex.Message });
            }
        }

        [HttpGet("{id}")]
       // [SwaggerOperation(Tags = new[] { "Role" })]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                var role = _roleRepository.GetRoleById(id);
                if (role == null)
                {
                    return NotFound(new { Message = "Role not found." });
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching the role.", Error = ex.Message });
            }
        }


        [HttpPut("{id}")]
       // [SwaggerOperation(Tags = new[] { "Role" })]
        public IActionResult UpdateRole(int id, [FromBody] Role role)
        {
            try
            {
                var existingRole = _roleRepository.GetRoleById(id);
                if (existingRole == null)
                {
                    return NotFound(new { Message = "Role not found." });
                }
                _roleRepository.UpdateRole(id, role);
                return Ok(new { Message = "Role updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the role.", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
       // [SwaggerOperation(Tags = new[] { "Role" })]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                var existingRole = _roleRepository.GetRoleById(id);
                if (existingRole == null)
                {
                    return NotFound(new { Message = "Role not found." });
                }
                _roleRepository.DeleteRole(id);
                return Ok(new { Message = "Role deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the role.", Error = ex.Message });
            }
        }
    }
}
