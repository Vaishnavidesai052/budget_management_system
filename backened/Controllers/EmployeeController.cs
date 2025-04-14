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
    public class EmployeeController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST: api/Employee (Create)
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.EmpName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "INSERT INTO tbl_employee (emp_code, emp_name, emp_dept, emp_designation, emp_post) VALUES (@EmpCode, @EmpName, @EmpDept, @EmpDesignation, @EmpPost); SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpCode", employee.EmpCode);
                        command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                        command.Parameters.AddWithValue("@EmpDept", employee.EmpDept ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpDesignation", employee.EmpDesignation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpPost", employee.EmpPost ?? (object)DBNull.Value);

                        var newId = await command.ExecuteScalarAsync();
                        employee.EmpId = Convert.ToInt32(newId);
                        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmpId }, employee);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/Employee (Fetch all employees)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = new List<Employee>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT emp_id, emp_code, emp_name, emp_dept, emp_designation, emp_post FROM tbl_employee";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                employees.Add(new Employee
                                {
                                    EmpId = reader.GetInt32("emp_id"),
                                    EmpCode = reader.GetInt32("emp_code"),
                                    EmpName = reader.GetString("emp_name"),
                                    EmpDept = reader.IsDBNull("emp_dept") ? (int?)null : reader.GetInt32("emp_dept"),
                                    EmpDesignation = reader.IsDBNull("emp_designation") ? null : reader.GetString("emp_designation"),
                                    EmpPost = reader.IsDBNull("emp_post") ? null : reader.GetString("emp_post")
                                });
                            }
                        }
                    }
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // ✅ Login Method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Employee loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.EmpEmail) || string.IsNullOrEmpty(loginRequest.EmpPassword))
                return BadRequest("Email and Password are required");

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM tbl_user_new WHERE email = @EmpEmail AND password = @EmpPassword";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmpEmail", loginRequest.EmpEmail);
                    command.Parameters.AddWithValue("@EmpPassword", loginRequest.EmpPassword);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var employee = new Employee
                            {
                                EmpId = reader.GetInt32("emp_id"),
                                EmpCode = reader.GetInt32("employee_code"),
                                EmpName = reader.GetString("employee_name"),
                                EmpDept = reader.IsDBNull("dept_id") ? null : reader.GetInt32("dept_id"),
                                EmpDesignation = reader.GetString("designation"),
                                EmpPost = reader.IsDBNull("post") ? null : reader.GetString("post"),
                                EmpEmail = reader.IsDBNull("email") ? null : reader.GetString("email"),
                                EmpRole = reader.IsDBNull("role_id") ? null : reader.GetInt32("role_id"),
                                EmpStatus = reader.IsDBNull("status") ? null : reader.GetString("status"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                UpdatedAt = reader.GetDateTime("updated_at")
                            };

                            return Ok(new
                            {
                                Message = "Login successful",
                                Employee = employee
                            });
                        }
                        else
                        {
                            return Unauthorized("Invalid email or password");
                        }
                    }
                }
            }
        }
                
                // GET: api/Employee/{id} (Fetch by ID)
                [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT emp_id, emp_code, emp_name, emp_dept, emp_designation, emp_post FROM tbl_employee WHERE emp_id = @EmpId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpId", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new Employee
                                {
                                    EmpId = reader.GetInt32("emp_id"),
                                    EmpCode = reader.GetInt32("emp_code"),
                                    EmpName = reader.GetString("emp_name"),
                                    EmpDept = reader.IsDBNull("emp_dept") ? (int?)null : reader.GetInt32("emp_dept"),
                                    EmpDesignation = reader.IsDBNull("emp_designation") ? null : reader.GetString("emp_designation"),
                                    EmpPost = reader.IsDBNull("emp_post") ? null : reader.GetString("emp_post")
                                });
                            }
                        }
                    }
                }
                return NotFound("Employee not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // PUT: api/Employee/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrEmpty(employee.EmpName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "UPDATE tbl_employee SET emp_code = @EmpCode, emp_name = @EmpName, emp_dept = @EmpDept, emp_designation = @EmpDesignation, emp_post = @EmpPost WHERE emp_id = @EmpId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpId", id);
                        command.Parameters.AddWithValue("@EmpCode", employee.EmpCode);
                        command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                        command.Parameters.AddWithValue("@EmpDept", employee.EmpDept ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpDesignation", employee.EmpDesignation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EmpPost", employee.EmpPost ?? (object)DBNull.Value);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Employee updated successfully.") : NotFound("Employee not found.");
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
