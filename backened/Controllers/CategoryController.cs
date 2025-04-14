using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManagementSystemNew.Model;
using System.Data;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string _connectionString;

        public CategoryController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST: api/Category (Create)
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.CategoryName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Insert into tbl_category
                    var query = "INSERT INTO tbl_category (category_name) VALUES (@CategoryName); SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                        var newId = await command.ExecuteScalarAsync();
                        category.CategoryId = Convert.ToInt32(newId);
                        return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/Category (Fetch all categories)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = new List<Category>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT category_id, category_name FROM tbl_category";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                categories.Add(new Category
                                {
                                    CategoryId = reader.GetInt32("category_id"),
                                    CategoryName = reader.GetString("category_name")
                                });
                            }
                        }
                    }
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/Category/{id} (Fetch by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT category_id, category_name FROM tbl_category WHERE category_id = @CategoryId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new Category
                                {
                                    CategoryId = reader.GetInt32("category_id"),
                                    CategoryName = reader.GetString("category_name")
                                });
                            }
                        }
                    }
                }
                return NotFound("Category not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // PUT: api/Category/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.CategoryName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "UPDATE tbl_category SET category_name = @CategoryName WHERE category_id = @CategoryId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", id);
                        command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Category updated successfully.") : NotFound("Category not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // DELETE: api/Category/{id} (Delete)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM tbl_category WHERE category_id = @CategoryId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", id);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Category deleted successfully.") : NotFound("Category not found.");
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
