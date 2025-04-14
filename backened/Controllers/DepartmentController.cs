using Microsoft.AspNetCore.Mvc;
using BudgetManagementSystemNew.Repositories;
using BudgetManagementSystemNew.Model;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // Add a new department
        [HttpPost]
        public IActionResult AddDepartment([FromBody] DepartmentModel department)
        {
            try
            {
                _departmentRepository.AddDepartment(department);
                return Ok(new { Message = "Department added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the department.", Error = ex.Message });
            }
        }

        // Get all departments
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                var departments = _departmentRepository.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching departments.", Error = ex.Message });
            }
        }

        // Get department by Id
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _departmentRepository.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound(new { Message = "Department not found." });
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching the department.", Error = ex.Message });
            }
        }

        // Update department
        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentModel department)
        {
            try
            {
                var existingDepartment = _departmentRepository.GetDepartmentById(id);
                if (existingDepartment == null)
                {
                    return NotFound(new { Message = "Department not found." });
                }
                _departmentRepository.UpdateDepartment(id, department);
                return Ok(new { Message = "Department updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the department.", Error = ex.Message });
            }
        }

        // Delete department
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var existingDepartment = _departmentRepository.GetDepartmentById(id);
                if (existingDepartment == null)
                {
                    return NotFound(new { Message = "Department not found." });
                }
                _departmentRepository.DeleteDepartment(id);
                return Ok(new { Message = "Department deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the department.", Error = ex.Message });
            }
        }
    }
}
