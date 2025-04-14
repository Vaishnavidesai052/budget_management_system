using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        private readonly string _connectionString;

        public FinancialYearController(IConfiguration configuration)
        {
            // Retrieve the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Endpoint to get Financial Year ID by Year (e.g., "2024-25")
        [HttpGet("getFinancialYearId/{year}")]
        public IActionResult GetFinancialYearId(string year)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    // Define your SQL query to fetch the financial year by Year string
                    string query = "SELECT id FROM tbl_financial_years WHERE yearRange = @Year";

                    // Create the command and set the parameters
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Year", year);

                    // Open the connection to the database
                    connection.Open();

                    // Execute the query and retrieve the result
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // If a financial year is found, return the Id
                        return Ok(Convert.ToInt32(result));
                    }
                    else
                    {
                        // If no financial year is found, return NotFound
                        return NotFound("Financial Year not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., database connection issues)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    
    // Endpoint to fetch all financial years
        [HttpGet("getAllFinancialYears")]
        public IActionResult GetAllFinancialYears()
        {
            try
            {
                List<string> financialYears = new List<string>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT yearRange FROM tbl_financial_years ORDER BY yearRange ASC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            financialYears.Add(reader["yearRange"].ToString());
                        }
                    }
                }

                return Ok(financialYears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        // Endpoint to get Financial Year by ID
        [HttpGet("getFinancialYear/{id}")]
        public IActionResult GetFinancialYear(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT yearRange FROM tbl_financial_years WHERE id = @Id";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Ok(result.ToString());
                    }
                    else
                    {
                        return NotFound("Financial Year not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
