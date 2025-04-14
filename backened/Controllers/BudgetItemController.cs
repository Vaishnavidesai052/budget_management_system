//namespace BudgetManagementSystemNew.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;
//using BudgetManagementSystemNew.Model;

//[Route("api/[controller]")]
//[ApiController]
//public class BudgetItemController : ControllerBase
//{
//    private readonly string _connectionString;

//    public BudgetItemController(IConfiguration configuration)
//    {
//        _connectionString = configuration.GetConnectionString("DefaultConnection");
//    }

//    // POST: api/BudgetItem
//    [HttpPost]
//    public async Task<ActionResult> CreateBudgetItem([FromBody] BudgetItem budgetItem)
//    {
//        var query = "INSERT INTO tbl_budgetitem (ItemName) VALUES (@ItemName)";

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
//                var rowsAffected = await command.ExecuteNonQueryAsync();

//                if (rowsAffected > 0)
//                {
//                    return Ok("Item added successfully.");
//                }
//                else
//                {
//                    return BadRequest("Failed to add item.");
//                }
//            }
//        }
//    }

//    // GET: api/BudgetItem
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<BudgetItem>>> GetBudgetItems()
//    {
//        var query = "SELECT ItemID, ItemName FROM tbl_budgetitem";
//        var budgetItems = new List<BudgetItem>();

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            using (var command = new MySqlCommand(query, connection))
//            using (var reader = await command.ExecuteReaderAsync())
//            {
//                while (await reader.ReadAsync())
//                {
//                    var item = new BudgetItem
//                    {
//                        ItemID = reader.GetInt32("ItemID"),
//                        ItemName = reader.GetString("ItemName")
//                    };
//                    budgetItems.Add(item);
//                }
//            }
//        }

//        return Ok(budgetItems);
//    }

//    // GET: api/BudgetItem/{id}
//    [HttpGet("{id}")]
//    public async Task<ActionResult<BudgetItem>> GetBudgetItemById(int id)
//    {
//        var query = "SELECT ItemID, ItemName FROM tbl_budgetitem WHERE ItemID = @ItemID";

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@ItemID", id);

//                using (var reader = await command.ExecuteReaderAsync())
//                {
//                    if (await reader.ReadAsync())
//                    {
//                        var item = new BudgetItem
//                        {
//                            ItemID = reader.GetInt32("ItemID"),
//                            ItemName = reader.GetString("ItemName")
//                        };
//                        return Ok(item); // Item found, return it
//                    }
//                    else
//                    {
//                        return NotFound("Item not found."); // If item is not found
//                    }
//                }
//            }
//        }
//    }

//    // DELETE: api/BudgetItem/{id}
//    [HttpDelete("{id}")]
//    public async Task<ActionResult> DeleteBudgetItem(int id)
//    {
//        var query = "DELETE FROM tbl_budgetitem WHERE ItemID = @ItemID";

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@ItemID", id);
//                var rowsAffected = await command.ExecuteNonQueryAsync();

//                if (rowsAffected > 0)
//                {
//                    return Ok("Item deleted successfully.");
//                }
//                else
//                {
//                    return NotFound("Item not found.");
//                }
//            }
//        }
//    }

//    // PUT: api/BudgetItem/{id}
//    [HttpPut("{id}")]
//    public async Task<ActionResult> UpdateBudgetItem(int id, [FromBody] BudgetItem budgetItem)
//    {
//        var query = "UPDATE tbl_budgetitem SET ItemName = @ItemName WHERE ItemID = @ItemID";

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            await connection.OpenAsync();

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@ItemID", id);
//                command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
//                var rowsAffected = await command.ExecuteNonQueryAsync();

//                if (rowsAffected > 0)
//                {
//                    return Ok("Item updated successfully.");
//                }
//                else
//                {
//                    return NotFound("Item not found.");
//                }
//            }
//        }
//    }
////}

//using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using BudgetManagementSystemNew.Model;
//using System.Data;

//namespace BudgetManagementSystemNew.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BudgetItemController : ControllerBase
//    {
//        private readonly string _connectionString;

//        public BudgetItemController(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//        }

//        // POST: api/BudgetItem (Create)
//        [HttpPost]
//        public async Task<ActionResult> CreateBudgetItem([FromBody] BudgetItem budgetItem)
//        {
//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                // Check if department exists
//                var checkDepartmentQuery = "SELECT COUNT(*) FROM tbl_department WHERE department_id = @department_id";
//                using (var checkCommand = new MySqlCommand(checkDepartmentQuery, connection))
//                {
//                    checkCommand.Parameters.AddWithValue("@department_id", budgetItem.department_id);
//                    var exists = (long)await checkCommand.ExecuteScalarAsync();

//                    if (exists == 0)
//                    {
//                        return BadRequest("Invalid department_id. The department does not exist.");
//                    }
//                }

//                // Insert Budget Item
//                var query = "INSERT INTO tbl_budgetitem (ItemName, department_id) VALUES (@ItemName, @department_id)";
//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
//                    command.Parameters.AddWithValue("@department_id", budgetItem.department_id);

//                    var rowsAffected = await command.ExecuteNonQueryAsync();
//                    return rowsAffected > 0 ? Ok("Item added successfully.") : BadRequest("Failed to add item.");
//                }
//            }
//        }


//        // GET: api/BudgetItem (Fetch All)
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<BudgetItem>>> GetBudgetItems()
//        {
//            var query = "SELECT ItemID, ItemName, department_id FROM tbl_budgetitem";
//            var budgetItems = new List<BudgetItem>();

//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand(query, connection))
//                using (var reader = await command.ExecuteReaderAsync())
//                {
//                    while (await reader.ReadAsync())
//                    {
//                        var item = new BudgetItem
//                        {
//                            ItemID = reader.GetInt32("ItemID"),
//                            ItemName = reader.GetString("ItemName"),
//                            department_id = reader.GetInt32("department_id") // Fetch department_id
//                        };
//                        budgetItems.Add(item);
//                    }
//                }
//            }

//            return Ok(budgetItems);
//        }

//        // GET: api/BudgetItem/{id} (Fetch by ID)
//        [HttpGet("{id}")]
//        public async Task<ActionResult<BudgetItem>> GetBudgetItemById(int id)
//        {
//            var query = "SELECT ItemID, ItemName, department_id FROM tbl_budgetitem WHERE ItemID = @ItemID";

//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@ItemID", id);

//                    using (var reader = await command.ExecuteReaderAsync())
//                    {
//                        if (await reader.ReadAsync())
//                        {
//                            var item = new BudgetItem
//                            {
//                                ItemID = reader.GetInt32("ItemID"),
//                                ItemName = reader.GetString("ItemName"),
//                                department_id = reader.GetInt32("department_id") // Fetch department_id
//                            };
//                            return Ok(item);
//                        }
//                        else
//                        {
//                            return NotFound("Item not found.");
//                        }
//                    }
//                }
//            }
//        }

//        // PUT: api/BudgetItem/{id} (Update)
//        [HttpPut("{id}")]
//        public async Task<ActionResult> UpdateBudgetItem(int id, [FromBody] BudgetItem budgetItem)
//        {
//            var query = "UPDATE tbl_budgetitem SET ItemName = @ItemName, department_id = @department_id WHERE ItemID = @ItemID";

//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@ItemID", id);
//                    command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
//                    command.Parameters.AddWithValue("@department_id", budgetItem.department_id); // Use department_id

//                    var rowsAffected = await command.ExecuteNonQueryAsync();

//                    if (rowsAffected > 0)
//                    {
//                        return Ok("Item updated successfully.");
//                    }
//                    else
//                    {
//                        return NotFound("Item not found.");
//                    }
//                }
//            }
//        }

//        // DELETE: api/BudgetItem/{id} (Delete)
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteBudgetItem(int id)
//        {
//            var query = "DELETE FROM tbl_budgetitem WHERE ItemID = @ItemID";

//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@ItemID", id);
//                    var rowsAffected = await command.ExecuteNonQueryAsync();

//                    if (rowsAffected > 0)
//                    {
//                        return Ok("Item deleted successfully.");
//                    }
//                    else
//                    {
//                        return NotFound("Item not found.");
//                    }
//                }
//            }
//        }
//    }
//}
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
    public class BudgetItemController : ControllerBase
    {
        private readonly string _connectionString;

        public BudgetItemController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // POST: api/BudgetItem (Create)
        [HttpPost]
        public async Task<ActionResult<BudgetItem>> CreateBudgetItem([FromBody] BudgetItem budgetItem)
        {
            if (budgetItem == null || string.IsNullOrEmpty(budgetItem.ItemName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Validate department_id exists (assuming the column in department table is 'id')
                    var checkDepartmentQuery = "SELECT COUNT(*) FROM department WHERE DepartmentID = @DepartmentID";
                    using (var checkCommand = new MySqlCommand(checkDepartmentQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@DepartmentID", budgetItem.department_id);
                        var exists = (long)await checkCommand.ExecuteScalarAsync();

                        if (exists == 0)
                            return BadRequest("Invalid department_id. The department does not exist.");
                    }

                    // Insert into tbl_budgetitem
                    var query = "INSERT INTO tbl_budgetitem (itemName, department_id) VALUES (@ItemName, @DepartmentID); SELECT LAST_INSERT_ID();";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
                        command.Parameters.AddWithValue("@DepartmentID", budgetItem.department_id);

                        var newId = await command.ExecuteScalarAsync();
                        budgetItem.ItemID = Convert.ToInt32(newId);
                        return CreatedAtAction(nameof(GetBudgetItemById), new { id = budgetItem.ItemID }, budgetItem);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // GET: api/BudgetItem (Fetch by department)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetItem>>> GetBudgetItems([FromQuery] int departmentId)
        {
            var budgetItems = new List<BudgetItem>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Modify the query to filter by department_id
                    var query = "SELECT ItemID, itemName, department_id FROM tbl_budgetitem WHERE department_id = @DepartmentID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentID", departmentId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                budgetItems.Add(new BudgetItem
                                {
                                    ItemID = reader.GetInt32("ItemID"),
                                    ItemName = reader.GetString("itemName"),
                                    department_id = reader.GetInt32("department_id")
                                });
                            }
                        }
                    }
                }
                return Ok(budgetItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


        // GET: api/BudgetItem/{id} (Fetch by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetItem>> GetBudgetItemById(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT ItemID, itemName, department_id FROM tbl_budgetitem WHERE ItemID = @ItemID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemID", id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return Ok(new BudgetItem
                                {
                                    ItemID = reader.GetInt32("ItemID"),
                                    ItemName = reader.GetString("itemName"),
                                    department_id = reader.GetInt32("department_id")
                                });
                            }
                        }
                    }
                }
                return NotFound("Item not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // PUT: api/BudgetItem/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBudgetItem(int id, [FromBody] BudgetItem budgetItem)
        {
            if (budgetItem == null || string.IsNullOrEmpty(budgetItem.ItemName))
                return BadRequest("Invalid data.");

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "UPDATE tbl_budgetitem SET itemName = @ItemName, department_id = @DepartmentID WHERE ItemID = @ItemID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemID", id);
                        command.Parameters.AddWithValue("@ItemName", budgetItem.ItemName);
                        command.Parameters.AddWithValue("@DepartmentID", budgetItem.department_id);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Item updated successfully.") : NotFound("Item not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // DELETE: api/BudgetItem/{id} (Delete)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBudgetItem(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM tbl_budgetitem WHERE ItemID = @ItemID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemID", id);
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? Ok("Item deleted successfully.") : NotFound("Item not found.");
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

