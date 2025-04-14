
//using BudgetManagementSystemNew.Model;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using MySql.Data.MySqlClient;
//using System.Collections.Generic;

//[Route("api/[controller]")]
//[ApiController]
//public class MonthlyEstimationController : ControllerBase
//{
//    private readonly string _connectionString;

//    public MonthlyEstimationController(IConfiguration configuration)
//    {
//        _connectionString = configuration.GetConnectionString("DefaultConnection");
//    }

//    // ✅ GET: api/MonthlyEstimation - Get all budget requests
//    [HttpGet]
//    public ActionResult<IEnumerable<MonthlyEstimation>> GetBudgetRequests()
//    {
//        var requests = new List<MonthlyEstimation>();

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "SELECT * FROM tbl_budgetrequests";
//            using (var command = new MySqlCommand(query, connection))
//            using (var reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    requests.Add(new MonthlyEstimation
//                    {
//                        Id = reader.GetInt32("Id"),
//                        RequestID = reader.GetInt32("RequestID"),
//                        ItemID = reader.GetInt32("ItemID"),
//                        FinancialYearID = reader.GetInt32("FinancialYearID"),
//                        DepartmentID = reader.GetInt32("department_id"), // ✅ Corrected
//                        Remarks = reader["Remarks"].ToString(),
//                        TotalAmount = reader.GetDecimal("TotalAmount"),
//                        Apr = reader.GetDecimal("Apr"),
//                        May = reader.GetDecimal("May"),
//                        Jun = reader.GetDecimal("Jun"),
//                        Jul = reader.GetDecimal("Jul"),
//                        Aug = reader.GetDecimal("Aug"),
//                        Sep = reader.GetDecimal("Sep"),
//                        Oct = reader.GetDecimal("Oct"),
//                        Nov = reader.GetDecimal("Nov"),
//                        Dec = reader.GetDecimal("Dec"),
//                        Jan = reader.GetDecimal("Jan"),
//                        Feb = reader.GetDecimal("Feb"),
//                        Mar = reader.GetDecimal("Mar")
//                    });
//                }
//            }
//        }

//        return Ok(requests);
//    }

//    // ✅ GET: api/MonthlyEstimation/{id} - Get a budget request by ID
//    [HttpGet("{id}")]
//    public ActionResult<MonthlyEstimation> GetBudgetRequestById(int id)
//    {
//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "SELECT * FROM tbl_budgetrequests WHERE Id = @Id";
//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);
//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        return Ok(new MonthlyEstimation
//                        {
//                            Id = reader.GetInt32("Id"),
//                            RequestID = reader.GetInt32("RequestID"),
//                            ItemID = reader.GetInt32("ItemID"),
//                            FinancialYearID = reader.GetInt32("FinancialYearID"),
//                            DepartmentID = reader.GetInt32("department_id"), // ✅ Corrected
//                            Remarks = reader["Remarks"].ToString(),
//                            TotalAmount = reader.GetDecimal("TotalAmount"),
//                            Apr = reader.GetDecimal("Apr"),
//                            May = reader.GetDecimal("May"),
//                            Jun = reader.GetDecimal("Jun"),
//                            Jul = reader.GetDecimal("Jul"),
//                            Aug = reader.GetDecimal("Aug"),
//                            Sep = reader.GetDecimal("Sep"),
//                            Oct = reader.GetDecimal("Oct"),
//                            Nov = reader.GetDecimal("Nov"),
//                            Dec = reader.GetDecimal("Dec"),
//                            Jan = reader.GetDecimal("Jan"),
//                            Feb = reader.GetDecimal("Feb"),
//                            Mar = reader.GetDecimal("Mar")
//                        });
//                    }
//                }
//            }
//        }

//        return NotFound();
//    }

//    // ✅ POST: api/MonthlyEstimation - Insert a new budget request
//    [HttpPost]
//    public IActionResult PostBudgetRequest(MonthlyEstimation estimation)
//    {
//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = @"INSERT INTO tbl_budgetrequests 
//              (RequestID, ItemID, FinancialYearID, department_id, Remarks, TotalAmount, 
//               Apr, May, Jun, Jul, Aug, Sep, Oct, `Dec`, Jan, Feb, Mar) 
//              VALUES (@RequestID, @ItemID, @FinancialYearID, @DepartmentID, @Remarks, @TotalAmount, 
//                      @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Dec, @Jan, @Feb, @Mar)";

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@RequestID", estimation.RequestID);
//                command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
//                command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
//                command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID); // ✅ Corrected
//                command.Parameters.AddWithValue("@Remarks", estimation.Remarks);
//                command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
//                command.Parameters.AddWithValue("@Apr", estimation.Apr);
//                command.Parameters.AddWithValue("@May", estimation.May);
//                command.Parameters.AddWithValue("@Jun", estimation.Jun);
//                command.Parameters.AddWithValue("@Jul", estimation.Jul);
//                command.Parameters.AddWithValue("@Aug", estimation.Aug);
//                command.Parameters.AddWithValue("@Sep", estimation.Sep);
//                command.Parameters.AddWithValue("@Oct", estimation.Oct);
//                command.Parameters.AddWithValue("@Nov", estimation.Nov);
//                command.Parameters.AddWithValue("@Dec", estimation.Dec);
//                command.Parameters.AddWithValue("@Jan", estimation.Jan);
//                command.Parameters.AddWithValue("@Feb", estimation.Feb);
//                command.Parameters.AddWithValue("@Mar", estimation.Mar);

//                command.ExecuteNonQuery();
//            }
//        }

//        return Ok(new { message = "Budget request added successfully!" });
//    }

//    // ✅ PUT: api/MonthlyEstimation/{id} - Update a budget request
//    [HttpPut("{id}")]
//    public IActionResult PutBudgetRequest(int id, MonthlyEstimation estimation)
//    {
//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = @"UPDATE tbl_budgetrequests SET 
//                          RequestID = @RequestID, 
//                          ItemID = @ItemID, 
//                          FinancialYearID = @FinancialYearID, 
//                          department_id = @DepartmentID, 
//                          Remarks = @Remarks, 
//                          TotalAmount = @TotalAmount, 
//                          Apr = @Apr, 
//                          May = @May, 
//                          Jun = @Jun, 
//                          Jul = @Jul, 
//                          Aug = @Aug, 
//                          Sep = @Sep, 
//                          Oct = @Oct, 
//                          Nov = @Nov, 
//                          Dec = @Dec, 
//                          Jan = @Jan, 
//                          Feb = @Feb, 
//                          Mar = @Mar 
//                          WHERE Id = @Id";

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);
//                command.Parameters.AddWithValue("@RequestID", estimation.RequestID);
//                command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
//                command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
//                command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID); // ✅ Corrected
//                command.Parameters.AddWithValue("@Remarks", estimation.Remarks);
//                command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
//                command.Parameters.AddWithValue("@Apr", estimation.Apr);
//                command.Parameters.AddWithValue("@May", estimation.May);
//                command.Parameters.AddWithValue("@Jun", estimation.Jun);
//                command.Parameters.AddWithValue("@Jul", estimation.Jul);
//                command.Parameters.AddWithValue("@Aug", estimation.Aug);
//                command.Parameters.AddWithValue("@Sep", estimation.Sep);
//                command.Parameters.AddWithValue("@Oct", estimation.Oct);
//                command.Parameters.AddWithValue("@Nov", estimation.Nov);
//                command.Parameters.AddWithValue("@Dec", estimation.Dec);
//                command.Parameters.AddWithValue("@Jan", estimation.Jan);
//                command.Parameters.AddWithValue("@Feb", estimation.Feb);
//                command.Parameters.AddWithValue("@Mar", estimation.Mar);

//                command.ExecuteNonQuery();
//            }
//        }

//        return Ok(new { message = "Budget request updated successfully!" });
//    }

//    [HttpPut("ByRequestID/{requestID}")]
//    public IActionResult UpdateBudgetRequestByRequestID(int requestID, [FromBody] List<MonthlyEstimation> estimations)
//    {
//        if (estimations == null || estimations.Count == 0)
//        {
//            return BadRequest(new { message = "Invalid estimation data. Please provide at least one item." });
//        }

//        try
//        {
//            using (var connection = new MySqlConnection(_connectionString))
//            {
//                connection.Open();
//                using (var transaction = connection.BeginTransaction()) // Use transaction for multiple updates
//                {
//                    foreach (var estimation in estimations)
//                    {
//                        var query = @"UPDATE tbl_budgetrequests SET 
//              ItemID = @ItemID, 
//              FinancialYearID = @FinancialYearID, 
//              department_id = @DepartmentID, 
//              Remarks = @Remarks, 
//              TotalAmount = @TotalAmount, 
//              `Apr` = @Apr, 
//              `May` = @May, 
//              `Jun` = @Jun, 
//              `Jul` = @Jul, 
//              `Aug` = @Aug, 
//              `Sep` = @Sep, 
//              `Oct` = @Oct, 
//              `Nov` = @Nov, 
//              `Dec` = @Dec,  -- ✅ Wrapped in backticks
//              `Jan` = @Jan,  -- ✅ Wrapped in backticks
//              `Feb` = @Feb, 
//              `Mar` = @Mar 
//              WHERE RequestID = @RequestID AND ItemID = @ItemID";


//                        using (var command = new MySqlCommand(query, connection, transaction))
//                        {
//                            command.Parameters.AddWithValue("@RequestID", requestID);
//                            command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
//                            command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
//                            command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID);
//                            command.Parameters.AddWithValue("@Remarks", estimation.Remarks ?? ""); // Avoid null issue
//                            command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
//                            command.Parameters.AddWithValue("@Apr", estimation.Apr);
//                            command.Parameters.AddWithValue("@May", estimation.May);
//                            command.Parameters.AddWithValue("@Jun", estimation.Jun);
//                            command.Parameters.AddWithValue("@Jul", estimation.Jul);
//                            command.Parameters.AddWithValue("@Aug", estimation.Aug);
//                            command.Parameters.AddWithValue("@Sep", estimation.Sep);
//                            command.Parameters.AddWithValue("@Oct", estimation.Oct);
//                            command.Parameters.AddWithValue("@Nov", estimation.Nov);
//                            command.Parameters.AddWithValue("@Dec", estimation.Dec);
//                            command.Parameters.AddWithValue("@Jan", estimation.Jan);
//                            command.Parameters.AddWithValue("@Feb", estimation.Feb);
//                            command.Parameters.AddWithValue("@Mar", estimation.Mar);

//                            int rowsAffected = command.ExecuteNonQuery();
//                            if (rowsAffected == 0)
//                            {
//                                transaction.Rollback();
//                                return NotFound(new { message = $"No budget request found for RequestID {requestID} and ItemID {estimation.ItemID}." });
//                            }
//                        }
//                    }
//                    transaction.Commit();
//                }
//            }

//            return Ok(new { message = "Budget estimations updated successfully!" });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new { message = "An error occurred while updating the budget estimations.", error = ex.Message });
//        }
//    }


//    // ✅ DELETE: api/MonthlyEstimation/{id} - Delete a budget request by ID
//    [HttpDelete("{id}")]
//    public IActionResult DeleteBudgetRequest(int id)
//    {
//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "DELETE FROM tbl_budgetrequests WHERE Id = @Id";

//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);

//                int rowsAffected = command.ExecuteNonQuery();
//                if (rowsAffected > 0)
//                {
//                    return Ok(new { message = "Budget request deleted successfully!" });
//                }
//                else
//                {
//                    return NotFound(new { message = "Budget request not found!" });
//                }
//            }
//        }
//    }
//    // ✅ GET: api/MonthlyEstimation/request/{requestId} - Get all budget items for a specific requestId
//    [HttpGet("request/{requestId}")]
//    public ActionResult<IEnumerable<MonthlyEstimation>> GetBudgetRequestsByRequestId(int requestId)
//    {
//        var requests = new List<MonthlyEstimation>();

//        using (var connection = new MySqlConnection(_connectionString))
//        {
//            connection.Open();
//            var query = "SELECT * FROM tbl_budgetrequests WHERE RequestID = @RequestID";
//            using (var command = new MySqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@RequestID", requestId);
//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        requests.Add(new MonthlyEstimation
//                        {
//                            Id = reader.GetInt32("Id"),
//                            RequestID = reader.GetInt32("RequestID"),
//                            ItemID = reader.GetInt32("ItemID"),
//                            FinancialYearID = reader.GetInt32("FinancialYearID"),
//                            DepartmentID = reader.GetInt32("department_id"),
//                            Remarks = reader["Remarks"].ToString(),
//                            TotalAmount = reader.GetDecimal("TotalAmount"),
//                            Apr = reader.GetDecimal("Apr"),
//                            May = reader.GetDecimal("May"),
//                            Jun = reader.GetDecimal("Jun"),
//                            Jul = reader.GetDecimal("Jul"),
//                            Aug = reader.GetDecimal("Aug"),
//                            Sep = reader.GetDecimal("Sep"),
//                            Oct = reader.GetDecimal("Oct"),
//                            Nov = reader.GetDecimal("Nov"),
//                            Dec = reader.GetDecimal("Dec"),
//                            Jan = reader.GetDecimal("Jan"),
//                            Feb = reader.GetDecimal("Feb"),
//                            Mar = reader.GetDecimal("Mar")
//                        });
//                    }
//                }
//            }
//        }

//        if (requests.Count == 0)
//        {
//            return NotFound(new { message = "No budget items found for this request ID." });
//        }

//        return Ok(requests);
//    }


//}
////using BudgetManagementSystemNew.Model;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.Extensions.Configuration;
////using MySql.Data.MySqlClient;
////using System.Collections.Generic;

////[Route("api/[controller]")]
////[ApiController]
////public class MonthlyEstimationController : ControllerBase
////{
////    private readonly string _connectionString;

////    public MonthlyEstimationController(IConfiguration configuration)
////    {
////        _connectionString = configuration.GetConnectionString("DefaultConnection");
////    }

////    [HttpGet]
////    public ActionResult<IEnumerable<MonthlyEstimation>> GetBudgetRequests()
////    {
////        var requests = new List<MonthlyEstimation>();

////        using (var connection = new MySqlConnection(_connectionString))
////        {
////            connection.Open();
////            var query = "SELECT * FROM tbl_budgetrequests";
////            using (var command = new MySqlCommand(query, connection))
////            using (var reader = command.ExecuteReader())
////            {
////                while (reader.Read())
////                {
////                    requests.Add(new MonthlyEstimation
////                    {
////                        Id = reader.GetInt32("Id"),
////                        RequestID = reader.GetInt32("RequestID"),
////                        ItemID = reader.GetInt32("ItemID"),
////                        FinancialYearID = reader.GetInt32("FinancialYearID"),
////                        DepartmentID = reader.GetInt32("department_id"),
////                        CategoryID = reader.GetInt32("category_id"),
////                        Remarks = reader["Remarks"].ToString(),
////                        TotalAmount = reader.GetDecimal("TotalAmount"),
////                        Apr = reader.GetDecimal("Apr"),
////                        May = reader.GetDecimal("May"),
////                        Jun = reader.GetDecimal("Jun"),
////                        Jul = reader.GetDecimal("Jul"),
////                        Aug = reader.GetDecimal("Aug"),
////                        Sep = reader.GetDecimal("Sep"),
////                        Oct = reader.GetDecimal("Oct"),
////                        Nov = reader.GetDecimal("Nov"),
////                        Dec = reader.GetDecimal("Dec"),
////                        Jan = reader.GetDecimal("Jan"),
////                        Feb = reader.GetDecimal("Feb"),
////                        Mar = reader.GetDecimal("Mar")
////                    });
////                }
////            }
////        }

////        return Ok(requests);
////    }

////    [HttpGet("{id}")]
////    public ActionResult<MonthlyEstimation> GetBudgetRequestById(int id)
////    {
////        using (var connection = new MySqlConnection(_connectionString))
////        {
////            connection.Open();
////            var query = "SELECT * FROM tbl_budgetrequests WHERE Id = @Id";
////            using (var command = new MySqlCommand(query, connection))
////            {
////                command.Parameters.AddWithValue("@Id", id);
////                using (var reader = command.ExecuteReader())
////                {
////                    if (reader.Read())
////                    {
////                        return Ok(new MonthlyEstimation
////                        {
////                            Id = reader.GetInt32("Id"),
////                            RequestID = reader.GetInt32("RequestID"),
////                            ItemID = reader.GetInt32("ItemID"),
////                            FinancialYearID = reader.GetInt32("FinancialYearID"),
////                            DepartmentID = reader.GetInt32("department_id"),
////                            CategoryID = reader.GetInt32("category_id"),
////                            Remarks = reader["Remarks"].ToString(),
////                            TotalAmount = reader.GetDecimal("TotalAmount"),
////                            Apr = reader.GetDecimal("Apr"),
////                            May = reader.GetDecimal("May"),
////                            Jun = reader.GetDecimal("Jun"),
////                            Jul = reader.GetDecimal("Jul"),
////                            Aug = reader.GetDecimal("Aug"),
////                            Sep = reader.GetDecimal("Sep"),
////                            Oct = reader.GetDecimal("Oct"),
////                            Nov = reader.GetDecimal("Nov"),
////                            Dec = reader.GetDecimal("Dec"),
////                            Jan = reader.GetDecimal("Jan"),
////                            Feb = reader.GetDecimal("Feb"),
////                            Mar = reader.GetDecimal("Mar")
////                        });
////                    }
////                }
////            }
////        }

////        return NotFound();
////    }

////    [HttpPost]
////    public IActionResult PostBudgetRequest(MonthlyEstimation estimation)
////    {
////        using (var connection = new MySqlConnection(_connectionString))
////        {
////            connection.Open();
////            var query = @"INSERT INTO tbl_budgetrequests 
////              (RequestID, ItemID, FinancialYearID, department_id, category_id, Remarks, TotalAmount, 
////               Apr, May, Jun, Jul, Aug, Sep, Oct, `Dec`, Jan, Feb, Mar) 
////              VALUES (@RequestID, @ItemID, @FinancialYearID, @DepartmentID, @CategoryID, @Remarks, @TotalAmount, 
////                      @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Dec, @Jan, @Feb, @Mar)";

////            using (var command = new MySqlCommand(query, connection))
////            {
////                command.Parameters.AddWithValue("@RequestID", estimation.RequestID);
////                command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
////                command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
////                command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID);
////                command.Parameters.AddWithValue("@CategoryID", estimation.CategoryID);
////                command.Parameters.AddWithValue("@Remarks", estimation.Remarks);
////                command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
////                command.Parameters.AddWithValue("@Apr", estimation.Apr);
////                command.Parameters.AddWithValue("@May", estimation.May);
////                command.Parameters.AddWithValue("@Jun", estimation.Jun);
////                command.Parameters.AddWithValue("@Jul", estimation.Jul);
////                command.Parameters.AddWithValue("@Aug", estimation.Aug);
////                command.Parameters.AddWithValue("@Sep", estimation.Sep);
////                command.Parameters.AddWithValue("@Oct", estimation.Oct);
////                command.Parameters.AddWithValue("@Nov", estimation.Nov);
////                command.Parameters.AddWithValue("@Dec", estimation.Dec);
////                command.Parameters.AddWithValue("@Jan", estimation.Jan);
////                command.Parameters.AddWithValue("@Feb", estimation.Feb);
////                command.Parameters.AddWithValue("@Mar", estimation.Mar);

////                command.ExecuteNonQuery();
////            }
////        }

////        return Ok(new { message = "Budget request added successfully!" });
////    }

////    [HttpDelete("{id}")]
////    public IActionResult DeleteBudgetRequest(int id)
////    {
////        using (var connection = new MySqlConnection(_connectionString))
////        {
////            connection.Open();
////            var query = "DELETE FROM tbl_budgetrequests WHERE Id = @Id";
////            using (var command = new MySqlCommand(query, connection))
////            {
////                command.Parameters.AddWithValue("@Id", id);
////                command.ExecuteNonQuery();
////            }
////        }
////        return Ok(new { message = "Budget request deleted successfully!" });
////    }
////}
using BudgetManagementSystemNew.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class MonthlyEstimationController : ControllerBase
{
    private readonly string _connectionString;

    public MonthlyEstimationController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // ✅ GET: api/MonthlyEstimation - Get all budget requests
    [HttpGet]
    public ActionResult<IEnumerable<MonthlyEstimation>> GetBudgetRequests()
    {
        var requests = new List<MonthlyEstimation>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM tbl_budgetrequests";
            using (var command = new MySqlCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    requests.Add(new MonthlyEstimation
                    {
                        Id = reader.GetInt32("Id"),
                        RequestID = reader.GetInt32("RequestID"),
                        ItemID = reader.GetInt32("ItemID"),
                        FinancialYearID = reader.GetInt32("FinancialYearID"),
                        DepartmentID = reader.GetInt32("department_id"),
                        CategoryID = reader.GetInt32("CategoryID"), // ✅ Added
                        Remarks = reader["Remarks"].ToString(),
                        TotalAmount = reader.GetDecimal("TotalAmount"),
                        Apr = reader.GetDecimal("Apr"),
                        May = reader.GetDecimal("May"),
                        Jun = reader.GetDecimal("Jun"),
                        Jul = reader.GetDecimal("Jul"),
                        Aug = reader.GetDecimal("Aug"),
                        Sep = reader.GetDecimal("Sep"),
                        Oct = reader.GetDecimal("Oct"),
                        Nov = reader.GetDecimal("Nov"),
                        Dec = reader.GetDecimal("Dec"),
                        Jan = reader.GetDecimal("Jan"),
                        Feb = reader.GetDecimal("Feb"),
                        Mar = reader.GetDecimal("Mar")
                    });
                }
            }
        }

        return Ok(requests);
    }

    // ✅ GET: api/MonthlyEstimation/{id} - Get a budget request by ID
    [HttpGet("{id}")]
    public ActionResult<MonthlyEstimation> GetBudgetRequestById(int id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM tbl_budgetrequests WHERE Id = @Id";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Ok(new MonthlyEstimation
                        {
                            Id = reader.GetInt32("Id"),
                            RequestID = reader.GetInt32("RequestID"),
                            ItemID = reader.GetInt32("ItemID"),
                            FinancialYearID = reader.GetInt32("FinancialYearID"),
                            DepartmentID = reader.GetInt32("department_id"),
                            CategoryID = reader.GetInt32("CategoryID"), // ✅ Added
                            Remarks = reader["Remarks"].ToString(),
                            TotalAmount = reader.GetDecimal("TotalAmount"),
                            Apr = reader.GetDecimal("Apr"),
                            May = reader.GetDecimal("May"),
                            Jun = reader.GetDecimal("Jun"),
                            Jul = reader.GetDecimal("Jul"),
                            Aug = reader.GetDecimal("Aug"),
                            Sep = reader.GetDecimal("Sep"),
                            Oct = reader.GetDecimal("Oct"),
                            Nov = reader.GetDecimal("Nov"),
                            Dec = reader.GetDecimal("Dec"),
                            Jan = reader.GetDecimal("Jan"),
                            Feb = reader.GetDecimal("Feb"),
                            Mar = reader.GetDecimal("Mar")
                        });
                    }
                }
            }
        }

        return NotFound();
    }

    // ✅ POST: api/MonthlyEstimation - Insert a new budget request
    [HttpPost]
    public IActionResult PostBudgetRequest(MonthlyEstimation estimation)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = @"INSERT INTO tbl_budgetrequests 
              (RequestID, ItemID, FinancialYearID, department_id, CategoryID, Remarks, TotalAmount, 
               Apr, May, Jun, Jul, Aug, Sep, Oct, `Dec`, Jan, Feb, Mar) 
              VALUES (@RequestID, @ItemID, @FinancialYearID, @DepartmentID, @CategoryID, @Remarks, @TotalAmount, 
                      @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Dec, @Jan, @Feb, @Mar)";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RequestID", estimation.RequestID);
                command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
                command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
                command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID);
                command.Parameters.AddWithValue("@CategoryID", estimation.CategoryID); // ✅ Added
                command.Parameters.AddWithValue("@Remarks", estimation.Remarks);
                command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
                command.Parameters.AddWithValue("@Apr", estimation.Apr);
                command.Parameters.AddWithValue("@May", estimation.May);
                command.Parameters.AddWithValue("@Jun", estimation.Jun);
                command.Parameters.AddWithValue("@Jul", estimation.Jul);
                command.Parameters.AddWithValue("@Aug", estimation.Aug);
                command.Parameters.AddWithValue("@Sep", estimation.Sep);
                command.Parameters.AddWithValue("@Oct", estimation.Oct);
                command.Parameters.AddWithValue("@Nov", estimation.Nov);
                command.Parameters.AddWithValue("@Dec", estimation.Dec);
                command.Parameters.AddWithValue("@Jan", estimation.Jan);
                command.Parameters.AddWithValue("@Feb", estimation.Feb);
                command.Parameters.AddWithValue("@Mar", estimation.Mar);

                command.ExecuteNonQuery();
            }
        }

        return Ok(new { message = "Budget request added successfully!" });
    }

    // ✅ PUT: api/MonthlyEstimation/{id} - Update a budget request
    [HttpPut("{id}")]
    public IActionResult PutBudgetRequest(int id, MonthlyEstimation estimation)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = @"UPDATE tbl_budgetrequests SET 
                          RequestID = @RequestID, 
                          ItemID = @ItemID, 
                          FinancialYearID = @FinancialYearID, 
                          department_id = @DepartmentID, 
                          CategoryID = @CategoryID, 
                          Remarks = @Remarks, 
                          TotalAmount = @TotalAmount, 
                          Apr = @Apr, 
                          May = @May, 
                          Jun = @Jun, 
                          Jul = @Jul, 
                          Aug = @Aug, 
                          Sep = @Sep, 
                          Oct = @Oct, 
                          Nov = @Nov, 
                          Dec = @Dec, 
                          Jan = @Jan, 
                          Feb = @Feb, 
                          Mar = @Mar 
                          WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@RequestID", estimation.RequestID);
                command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
                command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
                command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID);
                command.Parameters.AddWithValue("@CategoryID", estimation.CategoryID); // ✅ Added
                command.Parameters.AddWithValue("@Remarks", estimation.Remarks);
                command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
                command.Parameters.AddWithValue("@Apr", estimation.Apr);
                command.Parameters.AddWithValue("@May", estimation.May);
                command.Parameters.AddWithValue("@Jun", estimation.Jun);
                command.Parameters.AddWithValue("@Jul", estimation.Jul);
                command.Parameters.AddWithValue("@Aug", estimation.Aug);
                command.Parameters.AddWithValue("@Sep", estimation.Sep);
                command.Parameters.AddWithValue("@Oct", estimation.Oct);
                command.Parameters.AddWithValue("@Nov", estimation.Nov);
                command.Parameters.AddWithValue("@Dec", estimation.Dec);
                command.Parameters.AddWithValue("@Jan", estimation.Jan);
                command.Parameters.AddWithValue("@Feb", estimation.Feb);
                command.Parameters.AddWithValue("@Mar", estimation.Mar);

                command.ExecuteNonQuery();
            }
        }

        return Ok(new { message = "Budget request updated successfully!" });
    }

    // ✅ PUT: api/MonthlyEstimation/ByRequestID/{requestID} - Update multiple budget requests by RequestID
    [HttpPut("ByRequestID/{requestID}")]
    public IActionResult UpdateBudgetRequestByRequestID(int requestID, [FromBody] List<MonthlyEstimation> estimations)
    {
        if (estimations == null || estimations.Count == 0)
        {
            return BadRequest(new { message = "Invalid estimation data. Please provide at least one item." });
        }

        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var estimation in estimations)
                    {
                        var query = @"UPDATE tbl_budgetrequests SET 
                          ItemID = @ItemID, 
                          FinancialYearID = @FinancialYearID, 
                          department_id = @DepartmentID, 
                          category_id = @CategoryID, 
                          Remarks = @Remarks, 
                          TotalAmount = @TotalAmount, 
                          `Apr` = @Apr, 
                          `May` = @May, 
                          `Jun` = @Jun, 
                          `Jul` = @Jul, 
                          `Aug` = @Aug, 
                          `Sep` = @Sep, 
                          `Oct` = @Oct, 
                          `Nov` = @Nov, 
                          `Dec` = @Dec, 
                          `Jan` = @Jan, 
                          `Feb` = @Feb, 
                          `Mar` = @Mar 
                          WHERE RequestID = @RequestID AND ItemID = @ItemID";

                        using (var command = new MySqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@RequestID", requestID);
                            command.Parameters.AddWithValue("@ItemID", estimation.ItemID);
                            command.Parameters.AddWithValue("@FinancialYearID", estimation.FinancialYearID);
                            command.Parameters.AddWithValue("@DepartmentID", estimation.DepartmentID);
                            command.Parameters.AddWithValue("@CategoryID", estimation.CategoryID); // ✅ Added
                            command.Parameters.AddWithValue("@Remarks", estimation.Remarks ?? "");
                            command.Parameters.AddWithValue("@TotalAmount", estimation.TotalAmount);
                            command.Parameters.AddWithValue("@Apr", estimation.Apr);
                            command.Parameters.AddWithValue("@May", estimation.May);
                            command.Parameters.AddWithValue("@Jun", estimation.Jun);
                            command.Parameters.AddWithValue("@Jul", estimation.Jul);
                            command.Parameters.AddWithValue("@Aug", estimation.Aug);
                            command.Parameters.AddWithValue("@Sep", estimation.Sep);
                            command.Parameters.AddWithValue("@Oct", estimation.Oct);
                            command.Parameters.AddWithValue("@Nov", estimation.Nov);
                            command.Parameters.AddWithValue("@Dec", estimation.Dec);
                            command.Parameters.AddWithValue("@Jan", estimation.Jan);
                            command.Parameters.AddWithValue("@Feb", estimation.Feb);
                            command.Parameters.AddWithValue("@Mar", estimation.Mar);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return NotFound(new { message = $"No budget request found for RequestID {requestID} and ItemID {estimation.ItemID}." });
                            }
                        }
                    }
                    transaction.Commit();
                }
            }

            return Ok(new { message = "Budget estimations updated successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the budget estimations.", error = ex.Message });
        }
    }

    // ✅ DELETE: api/MonthlyEstimation/{id} - Delete a budget request by ID
    [HttpDelete("{id}")]
    public IActionResult DeleteBudgetRequest(int id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM tbl_budgetrequests WHERE Id = @Id";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Budget request deleted successfully!" });
                }
                else
                {
                    return NotFound(new { message = "Budget request not found!" });
                }
            }
        }
    }

    // ✅ GET: api/MonthlyEstimation/request/{requestId} - Get all budget items for a specific requestId
    [HttpGet("request/{requestId}")]
    public ActionResult<IEnumerable<MonthlyEstimation>> GetBudgetRequestsByRequestId(int requestId)
    {
        var requests = new List<MonthlyEstimation>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM tbl_budgetrequests WHERE RequestID = @RequestID";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RequestID", requestId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests.Add(new MonthlyEstimation
                        {
                            Id = reader.GetInt32("Id"),
                            RequestID = reader.GetInt32("RequestID"),
                            ItemID = reader.GetInt32("ItemID"),
                            FinancialYearID = reader.GetInt32("FinancialYearID"),
                            DepartmentID = reader.GetInt32("department_id"),
                            CategoryID = reader.GetInt32("category_id"), // ✅ Added
                            Remarks = reader["Remarks"].ToString(),
                            TotalAmount = reader.GetDecimal("TotalAmount"),
                            Apr = reader.GetDecimal("Apr"),
                            May = reader.GetDecimal("May"),
                            Jun = reader.GetDecimal("Jun"),
                            Jul = reader.GetDecimal("Jul"),
                            Aug = reader.GetDecimal("Aug"),
                            Sep = reader.GetDecimal("Sep"),
                            Oct = reader.GetDecimal("Oct"),
                            Nov = reader.GetDecimal("Nov"),
                            Dec = reader.GetDecimal("Dec"),
                            Jan = reader.GetDecimal("Jan"),
                            Feb = reader.GetDecimal("Feb"),
                            Mar = reader.GetDecimal("Mar")
                        });
                    }
                }
            }
        }

        if (requests.Count == 0)
        {
            return NotFound(new { message = "No budget items found for this request ID." });
        }

        return Ok(requests);
    }
}