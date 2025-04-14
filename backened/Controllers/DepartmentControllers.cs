using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using BudgetManagementSystemNew.Model;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentControllers : ControllerBase
    {
        private readonly string _connectionString;

        public DepartmentControllers(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST: api/Department (Create a new department)
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.DeptName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO tbl_dept (dept_name) VALUES (@DeptName); SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeptName", department.DeptName);
                        var newId = await command.ExecuteScalarAsync();
                        department.DeptId = Convert.ToInt32(newId);
                        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DeptId }, department);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/Department (Fetch all departments)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = new List<Department>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT dept_id, dept_name FROM tbl_dept";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                departments.Add(new Department
                                {
                                    DeptId = reader.GetInt32("dept_id"),
                                    DeptName = reader.GetString("dept_name")
                                });
                            }
                        }
                    }
                }
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/Department/{id} (Fetch a department by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT dept_id, dept_name FROM tbl_dept WHERE dept_id = @DeptId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeptId", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new Department
                                {
                                    DeptId = reader.GetInt32("dept_id"),
                                    DeptName = reader.GetString("dept_name")
                                });
                            }
                        }
                    }
                }
                return NotFound("Department not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // PUT: api/Department/{id} (Update a department)
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.DeptName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "UPDATE tbl_dept SET dept_name = @DeptName WHERE dept_id = @DeptId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeptId", id);
                        command.Parameters.AddWithValue("@DeptName", department.DeptName);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Department updated successfully.") : NotFound("Department not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // DELETE: api/Department/{id} (Delete a department)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM tbl_dept WHERE dept_id = @DeptId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DeptId", id);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Department deleted successfully.") : NotFound("Department not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
