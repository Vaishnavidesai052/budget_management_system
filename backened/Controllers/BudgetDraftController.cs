using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using BudgetManagementSystemNew.Models;
using Microsoft.Extensions.Configuration;
using BudgetManagementSystemNew.DTOs;
using Newtonsoft.Json;
using BudgetManagementSystemNew.Model;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetDraftsController : ControllerBase
    {
        private readonly string _connectionString;

        public BudgetDraftsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<BudgetDraft> drafts = new List<BudgetDraft>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM tbl_budgetdrafts";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drafts.Add(new BudgetDraft
                        {
                            DraftID = reader.GetInt32("draftID"),
                            RequestID = reader.IsDBNull("requestID") ? (int?)null : reader.GetInt32("requestID"),
                            DepartmentID = reader.GetInt32("departmentID"),
                            ItemID = reader.GetInt32("itemID"),
                            CategoryID = reader.GetInt32("category_id"),
                            FinancialYearID = reader.GetInt32("financialYearID"),
                            TotalAmount = reader.GetDecimal("totalAmount"),
                            Remarks = reader.IsDBNull("remarks") ? null : reader.GetString("remarks"),
                            Status = reader.GetString("status"),
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
            return Ok(drafts);

        }

        //[HttpPost]
        //public IActionResult Create(BudgetDraft draft)
        //{
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        // Check if itemID exists in tbl_budgetitem
        //        string itemCheckQuery = "SELECT COUNT(*) FROM tbl_budgetitem WHERE ItemID = @itemID";
        //        MySqlCommand itemCheckCmd = new MySqlCommand(itemCheckQuery, connection);
        //        itemCheckCmd.Parameters.AddWithValue("@itemID", draft.ItemID);
        //        int itemCount = Convert.ToInt32(itemCheckCmd.ExecuteScalar());

        //        if (itemCount == 0)
        //        {
        //            return BadRequest($"Item with ID {draft.ItemID} does not exist in the budget items table.");
        //        }

        //        // Proceed with the existing logic for checking and inserting/updating the draft
        //        string checkQuery = @"SELECT COUNT(*) FROM tbl_budgetdrafts 
        //              WHERE departmentID = @departmentID 
        //              AND itemID = @itemID 
        //              AND category_id = @categoryID 
        //              AND financialYearID = @financialYearID";
        //        MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
        //        checkCmd.Parameters.AddWithValue("@departmentID", draft.DepartmentID);
        //        checkCmd.Parameters.AddWithValue("@itemID", draft.ItemID);
        //        checkCmd.Parameters.AddWithValue("@categoryID", draft.CategoryID);
        //        checkCmd.Parameters.AddWithValue("@financialYearID", draft.FinancialYearID);

        //        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

        //        if (count > 0)
        //        {
        //            // Update existing record
        //            string updateQuery = @"UPDATE tbl_budgetdrafts 
        //                   SET totalAmount = @totalAmount, 
        //                       remarks = @remarks, 
        //                       status = @status, 
        //                       `Apr` = @Apr, 
        //                       `May` = @May, 
        //                       `Jun` = @Jun, 
        //                       `Jul` = @Jul, 
        //                       `Aug` = @Aug, 
        //                       `Sep` = @Sep, 
        //                       `Oct` = @Oct, 
        //                       `Nov` = @Nov, 
        //                       `Dec` = @Dec, 
        //                       `Jan` = @Jan, 
        //                       `Feb` = @Feb, 
        //                       `Mar` = @Mar 
        //                   WHERE departmentID = @departmentID 
        //                   AND itemID = @itemID 
        //                   AND category_id = @categoryID 
        //                   AND financialYearID = @financialYearID";

        //            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
        //            updateCmd.Parameters.AddWithValue("@totalAmount", draft.TotalAmount);
        //            updateCmd.Parameters.AddWithValue("@remarks", draft.Remarks);
        //            updateCmd.Parameters.AddWithValue("@status", draft.Status);
        //            updateCmd.Parameters.AddWithValue("@Apr", draft.Apr);
        //            updateCmd.Parameters.AddWithValue("@May", draft.May);
        //            updateCmd.Parameters.AddWithValue("@Jun", draft.Jun);
        //            updateCmd.Parameters.AddWithValue("@Jul", draft.Jul);
        //            updateCmd.Parameters.AddWithValue("@Aug", draft.Aug);
        //            updateCmd.Parameters.AddWithValue("@Sep", draft.Sep);
        //            updateCmd.Parameters.AddWithValue("@Oct", draft.Oct);
        //            updateCmd.Parameters.AddWithValue("@Nov", draft.Nov);
        //            updateCmd.Parameters.AddWithValue("@Dec", draft.Dec);
        //            updateCmd.Parameters.AddWithValue("@Jan", draft.Jan);
        //            updateCmd.Parameters.AddWithValue("@Feb", draft.Feb);
        //            updateCmd.Parameters.AddWithValue("@Mar", draft.Mar);
        //            updateCmd.Parameters.AddWithValue("@departmentID", draft.DepartmentID);
        //            updateCmd.Parameters.AddWithValue("@itemID", draft.ItemID);
        //            updateCmd.Parameters.AddWithValue("@categoryID", draft.CategoryID);
        //            updateCmd.Parameters.AddWithValue("@financialYearID", draft.FinancialYearID);

        //            updateCmd.ExecuteNonQuery();
        //            return Ok("Budget draft updated successfully.");
        //        }
        //        else
        //        {
        //            // Insert new record
        //            string insertQuery = @"INSERT INTO tbl_budgetdrafts 
        //                   (departmentID, itemID, category_id, financialYearID, totalAmount, remarks, status, 
        //                   `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`) 
        //                   VALUES (@departmentID, @itemID, @categoryID, @financialYearID, @totalAmount, @remarks, @status, 
        //                   @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Nov, @Dec, @Jan, @Feb, @Mar)";

        //            MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
        //            cmd.Parameters.AddWithValue("@departmentID", draft.DepartmentID);
        //            cmd.Parameters.AddWithValue("@itemID", draft.ItemID);
        //            cmd.Parameters.AddWithValue("@categoryID", draft.CategoryID);
        //            cmd.Parameters.AddWithValue("@financialYearID", draft.FinancialYearID);
        //            cmd.Parameters.AddWithValue("@totalAmount", draft.TotalAmount);
        //            cmd.Parameters.AddWithValue("@remarks", draft.Remarks);
        //            cmd.Parameters.AddWithValue("@status", draft.Status);
        //            cmd.Parameters.AddWithValue("@Apr", draft.Apr);
        //            cmd.Parameters.AddWithValue("@May", draft.May);
        //            cmd.Parameters.AddWithValue("@Jun", draft.Jun);
        //            cmd.Parameters.AddWithValue("@Jul", draft.Jul);
        //            cmd.Parameters.AddWithValue("@Aug", draft.Aug);
        //            cmd.Parameters.AddWithValue("@Sep", draft.Sep);
        //            cmd.Parameters.AddWithValue("@Oct", draft.Oct);
        //            cmd.Parameters.AddWithValue("@Nov", draft.Nov);
        //            cmd.Parameters.AddWithValue("@Dec", draft.Dec);
        //            cmd.Parameters.AddWithValue("@Jan", draft.Jan);
        //            cmd.Parameters.AddWithValue("@Feb", draft.Feb);
        //            cmd.Parameters.AddWithValue("@Mar", draft.Mar);

        //            cmd.ExecuteNonQuery();
        //            return Ok("Budget draft created successfully.");
        //        }
        //    }
        //}

        [HttpPost]
        public IActionResult Create(BudgetDraft draft)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Check if itemID exists in tbl_budgetitem
                string itemCheckQuery = "SELECT COUNT(*) FROM tbl_budgetitem WHERE ItemID = @itemID";
                MySqlCommand itemCheckCmd = new MySqlCommand(itemCheckQuery, connection);
                itemCheckCmd.Parameters.AddWithValue("@itemID", draft.ItemID);
                int itemCount = Convert.ToInt32(itemCheckCmd.ExecuteScalar());

                if (itemCount == 0)
                {
                    return BadRequest($"Item with ID {draft.ItemID} does not exist in the budget items table.");
                }

                // Check if we have a draftID (for updates)
                if (draft.DraftID > 0)
                {
                    // Update existing record by draftID
                    string updateQuery = @"UPDATE tbl_budgetdrafts 
                   SET totalAmount = @totalAmount, 
                       remarks = @remarks, 
                       status = @status, 
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
                   WHERE draftID = @draftID";

                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@draftID", draft.DraftID);
                    updateCmd.Parameters.AddWithValue("@totalAmount", draft.TotalAmount);
                    updateCmd.Parameters.AddWithValue("@remarks", draft.Remarks);
                    updateCmd.Parameters.AddWithValue("@status", draft.Status);
                    updateCmd.Parameters.AddWithValue("@Apr", draft.Apr);
                    updateCmd.Parameters.AddWithValue("@May", draft.May);
                    updateCmd.Parameters.AddWithValue("@Jun", draft.Jun);
                    updateCmd.Parameters.AddWithValue("@Jul", draft.Jul);
                    updateCmd.Parameters.AddWithValue("@Aug", draft.Aug);
                    updateCmd.Parameters.AddWithValue("@Sep", draft.Sep);
                    updateCmd.Parameters.AddWithValue("@Oct", draft.Oct);
                    updateCmd.Parameters.AddWithValue("@Nov", draft.Nov);
                    updateCmd.Parameters.AddWithValue("@Dec", draft.Dec);
                    updateCmd.Parameters.AddWithValue("@Jan", draft.Jan);
                    updateCmd.Parameters.AddWithValue("@Feb", draft.Feb);
                    updateCmd.Parameters.AddWithValue("@Mar", draft.Mar);

                    updateCmd.ExecuteNonQuery();
                    return Ok("Budget draft updated successfully.");
                }
                else
                {
                    // Check if this exact draft already exists (for new items)
                    string checkQuery = @"SELECT COUNT(*) FROM tbl_budgetdrafts 
                  WHERE departmentID = @departmentID 
                  AND itemID = @itemID 
                  AND category_id = @categoryID 
                  AND financialYearID = @financialYearID";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@departmentID", draft.DepartmentID);
                    checkCmd.Parameters.AddWithValue("@itemID", draft.ItemID);
                    checkCmd.Parameters.AddWithValue("@categoryID", draft.CategoryID);
                    checkCmd.Parameters.AddWithValue("@financialYearID", draft.FinancialYearID);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return Conflict("This budget draft already exists.");
                    }

                    // Insert new record
                    string insertQuery = @"INSERT INTO tbl_budgetdrafts 
                   (departmentID, itemID, category_id, financialYearID, totalAmount, remarks, status, 
                   `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`) 
                   VALUES (@departmentID, @itemID, @categoryID, @financialYearID, @totalAmount, @remarks, @status, 
                   @Apr, @May, @Jun, @Jul, @Aug, @Sep, @Oct, @Nov, @Dec, @Jan, @Feb, @Mar);
                   SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@departmentID", draft.DepartmentID);
                    cmd.Parameters.AddWithValue("@itemID", draft.ItemID);
                    cmd.Parameters.AddWithValue("@categoryID", draft.CategoryID);
                    cmd.Parameters.AddWithValue("@financialYearID", draft.FinancialYearID);
                    cmd.Parameters.AddWithValue("@totalAmount", draft.TotalAmount);
                    cmd.Parameters.AddWithValue("@remarks", draft.Remarks);
                    cmd.Parameters.AddWithValue("@status", draft.Status);
                    cmd.Parameters.AddWithValue("@Apr", draft.Apr);
                    cmd.Parameters.AddWithValue("@May", draft.May);
                    cmd.Parameters.AddWithValue("@Jun", draft.Jun);
                    cmd.Parameters.AddWithValue("@Jul", draft.Jul);
                    cmd.Parameters.AddWithValue("@Aug", draft.Aug);
                    cmd.Parameters.AddWithValue("@Sep", draft.Sep);
                    cmd.Parameters.AddWithValue("@Oct", draft.Oct);
                    cmd.Parameters.AddWithValue("@Nov", draft.Nov);
                    cmd.Parameters.AddWithValue("@Dec", draft.Dec);
                    cmd.Parameters.AddWithValue("@Jan", draft.Jan);
                    cmd.Parameters.AddWithValue("@Feb", draft.Feb);
                    cmd.Parameters.AddWithValue("@Mar", draft.Mar);

                    // Execute and return the new draftID
                    int newDraftID = Convert.ToInt32(cmd.ExecuteScalar());
                    return Ok(new { message = "Budget draft created successfully.", draftID = newDraftID });
                }
            }
        }
        [HttpPut]
        public IActionResult UpdateBudgetDraft(int draftID, [FromBody] BudgetDraft updatedDraft)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = @"UPDATE tbl_budgetdrafts SET 
                        requestID = @RequestID, 
                        departmentID = @DepartmentID, 
                        itemID = @ItemID, 
                        category_id = @CategoryID, 
                        financialYearID = @FinancialYearID, 
                        totalAmount = @TotalAmount, 
                        remarks = @Remarks, 
                        status = @Status, 
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
                        WHERE draftID = @DraftID";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DraftID", draftID);
                cmd.Parameters.AddWithValue("@RequestID", (object?)updatedDraft.RequestID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DepartmentID", updatedDraft.DepartmentID);
                cmd.Parameters.AddWithValue("@ItemID", updatedDraft.ItemID);
                cmd.Parameters.AddWithValue("@CategoryID", updatedDraft.CategoryID);
                cmd.Parameters.AddWithValue("@FinancialYearID", updatedDraft.FinancialYearID);
                cmd.Parameters.AddWithValue("@TotalAmount", updatedDraft.TotalAmount);
                cmd.Parameters.AddWithValue("@Remarks", (object?)updatedDraft.Remarks ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", updatedDraft.Status);
                cmd.Parameters.AddWithValue("@Apr", updatedDraft.Apr);
                cmd.Parameters.AddWithValue("@May", updatedDraft.May);
                cmd.Parameters.AddWithValue("@Jun", updatedDraft.Jun);
                cmd.Parameters.AddWithValue("@Jul", updatedDraft.Jul);
                cmd.Parameters.AddWithValue("@Aug", updatedDraft.Aug);
                cmd.Parameters.AddWithValue("@Sep", updatedDraft.Sep);
                cmd.Parameters.AddWithValue("@Oct", updatedDraft.Oct);
                cmd.Parameters.AddWithValue("@Nov", updatedDraft.Nov);
                cmd.Parameters.AddWithValue("@Dec", updatedDraft.Dec);
                cmd.Parameters.AddWithValue("@Jan", updatedDraft.Jan);
                cmd.Parameters.AddWithValue("@Feb", updatedDraft.Feb);
                cmd.Parameters.AddWithValue("@Mar", updatedDraft.Mar);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok(new { message = "Budget draft updated successfully." });
                }
                else
                {
                    return NotFound(new { message = "Budget draft not found." });
                }
            }
        }

        [HttpDelete("{draftID}")]
        public IActionResult Delete(int draftID)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    // SQL query to delete a record by draftID
                    string query = "DELETE FROM tbl_budgetdrafts WHERE draftID = @DraftID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Add the draftID parameter to the query
                    cmd.Parameters.AddWithValue("@DraftID", draftID);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if any rows were affected (i.e., if the record was deleted)
                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "Budget draft deleted successfully." });
                    }
                    else
                    {
                        return NotFound(new { message = "Budget draft not found." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An error occurred while deleting the budget draft.", error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BudgetDraft draft = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM tbl_budgetdrafts WHERE draftID = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        draft = new BudgetDraft
                        {
                            DraftID = reader.GetInt32("draftID"),
                            RequestID = reader.IsDBNull("requestID") ? (int?)null : reader.GetInt32("requestID"),
                            DepartmentID = reader.GetInt32("departmentID"),
                            ItemID = reader.GetInt32("itemID"),
                            FinancialYearID = reader.GetInt32("financialYearID"),
                            CategoryID = reader.GetInt32("categoryID"), // ✅ Added CategoryID
                            TotalAmount = reader.GetDecimal("totalAmount"),
                            Remarks = reader.IsDBNull("remarks") ? null : reader.GetString("remarks"),
                            Status = reader.GetString("status"),
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
                        };
                    }
                }
            }
            return draft == null ? NotFound() : Ok(draft);
        }
        //[HttpPut("submit/{departmentId}/{financialYearId}")]
        //public IActionResult SubmitToHOD(int departmentId, int financialYearId,string Username,)
        //[HttpPost("submit-to-Hod")]
        //public IActionResult SubmitToHOD([FromBody] SubmitToHODDto dto)
        //{
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Log incoming data
        //                Console.WriteLine($"Received DTO: {JsonConvert.SerializeObject(dto)}");

        //                // Generate a new requestID
        //                string requestIdQuery = "SELECT COALESCE(MAX(requestID), 0) + 1 FROM tbl_request_actions";
        //                MySqlCommand getRequestIdCmd = new MySqlCommand(requestIdQuery, connection, transaction);
        //                int newRequestId = Convert.ToInt32(getRequestIdCmd.ExecuteScalar());

        //                // Insert into tbl_request_actions
        //                string insertRequestActionQuery = @"
        //            INSERT INTO tbl_request_actions 
        //            (requestID, requestedBy, requestedTo, status, remark, requestedAt) 
        //            VALUES 
        //            (@requestID, @requestedBy, @requestedTo, @status, @remark, NOW())";

        //                MySqlCommand insertRequestActionCmd = new MySqlCommand(insertRequestActionQuery, connection, transaction);
        //                insertRequestActionCmd.Parameters.AddWithValue("@requestID", newRequestId);
        //                insertRequestActionCmd.Parameters.AddWithValue("@requestedBy", dto.requestedBy);
        //                insertRequestActionCmd.Parameters.AddWithValue("@requestedTo", dto.requestedTo);
        //                insertRequestActionCmd.Parameters.AddWithValue("@status", "Pending");
        //                insertRequestActionCmd.Parameters.AddWithValue("@remark", "Budget request submitted");
        //                insertRequestActionCmd.ExecuteNonQuery();

        //                // Check if there are drafts to insert
        //                string checkDraftQuery = "SELECT COUNT(*) FROM tbl_budgetdrafts WHERE departmentID = @department_id AND financialYearID = @financialYearID AND requestID IS NULL";
        //                MySqlCommand checkDraftCmd = new MySqlCommand(checkDraftQuery, connection, transaction);
        //                checkDraftCmd.Parameters.AddWithValue("@department_id", dto.departmentId);
        //                checkDraftCmd.Parameters.AddWithValue("@financialYearID", dto.financialYearId);
        //                int draftCount = Convert.ToInt32(checkDraftCmd.ExecuteScalar());
        //                Console.WriteLine($"Found {draftCount} drafts to submit.");

        //                // Insert into tbl_budgetrequests
        //                string insertRequestQuery = @"
        //            INSERT INTO tbl_budgetrequests 
        //            (requestID, department_id, financialYearID, category_id, itemID, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`, totalAmount, remarks, statusID, createdAt)
        //            SELECT @requestID, departmentID, financialYearID, category_id, itemID, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`, totalAmount, remarks, 1, NOW()
        //            FROM tbl_budgetdrafts
        //            WHERE departmentID = @department_id AND financialYearID = @financialYearID AND requestID IS NULL";

        //                MySqlCommand insertCmd = new MySqlCommand(insertRequestQuery, connection, transaction);
        //                insertCmd.Parameters.AddWithValue("@requestID", newRequestId);
        //                insertCmd.Parameters.AddWithValue("@department_id", dto.departmentId);
        //                insertCmd.Parameters.AddWithValue("@financialYearID", dto.financialYearId);
        //                int insertedRows = insertCmd.ExecuteNonQuery();

        //                // Update tbl_budgetdrafts with the new requestID
        //                string updateDraftQuery = @"
        //            UPDATE tbl_budgetdrafts 
        //            SET requestID = @requestID, status = 'submitted' 
        //            WHERE departmentID = @department_id AND financialYearID = @financialYearID AND requestID IS NULL";

        //                MySqlCommand updateCmd = new MySqlCommand(updateDraftQuery, connection, transaction);
        //                updateCmd.Parameters.AddWithValue("@requestID", newRequestId);
        //                updateCmd.Parameters.AddWithValue("@department_id", dto.departmentId);
        //                updateCmd.Parameters.AddWithValue("@financialYearID", dto.financialYearId);
        //                int updatedRows = updateCmd.ExecuteNonQuery();

        //                transaction.Commit();

        //                Console.WriteLine($"Rows inserted into tbl_budgetrequests: {insertedRows}");
        //                Console.WriteLine($"Rows updated in tbl_budgetdrafts: {updatedRows}");

        //                return insertedRows > 0 && updatedRows > 0 ? Ok("Budget drafts submitted successfully.") : NotFound("No budget drafts found for submission.");
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                return StatusCode(500, $"An error occurred: {ex.Message}");
        //            }
        //        }
        //    }


        [HttpPost("submit-to-Hod")]
        public IActionResult SubmitToHOD([FromBody] SubmitToHODDto dto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Generate a new requestID
                        string requestIdQuery = "SELECT COALESCE(MAX(requestID), 0) + 1 FROM tbl_request_actions";
                        MySqlCommand getRequestIdCmd = new MySqlCommand(requestIdQuery, connection, transaction);
                        int newRequestId = Convert.ToInt32(getRequestIdCmd.ExecuteScalar());

                        // Insert into tbl_request_actions
                        string insertRequestActionQuery = @"
                        INSERT INTO tbl_request_actions 
                        (requestID, requestedBy, requestedTo, status, remark, requestedAt) 
                        VALUES 
                        (@requestID, @requestedBy, @requestedTo, @status, @remark, NOW())";

                        MySqlCommand insertRequestActionCmd = new MySqlCommand(insertRequestActionQuery, connection, transaction);
                        insertRequestActionCmd.Parameters.AddWithValue("@requestID", newRequestId);
                        insertRequestActionCmd.Parameters.AddWithValue("@requestedBy", dto.requestedBy);
                        insertRequestActionCmd.Parameters.AddWithValue("@requestedTo", dto.requestedTo);
                        insertRequestActionCmd.Parameters.AddWithValue("@status", "Pending");
                        insertRequestActionCmd.Parameters.AddWithValue("@remark", "Budget request submitted");
                        insertRequestActionCmd.ExecuteNonQuery();

                        // Add to request history
                        string historyQuery = @"
                        INSERT INTO request_history 
                        (request_id, action, performed_by, performed_to, remarks) 
                        VALUES 
                        (@requestID, 'Submitted', @requestedBy, @requestedTo, 'Initial submission')";

                        MySqlCommand historyCmd = new MySqlCommand(historyQuery, connection, transaction);
                        historyCmd.Parameters.AddWithValue("@requestID", newRequestId);
                        historyCmd.Parameters.AddWithValue("@requestedBy", dto.requestedBy);
                        historyCmd.Parameters.AddWithValue("@requestedTo", dto.requestedTo);
                        historyCmd.ExecuteNonQuery();

                        // Insert into tbl_budgetrequests
                        string insertRequestQuery = @"
                        INSERT INTO tbl_budgetrequests 
                        (requestID, department_id, financialYearID, category_id, itemID, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`, totalAmount, remarks, statusID, createdAt)
                        SELECT @requestID, departmentID, financialYearID, category_id, itemID, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`, `Jan`, `Feb`, `Mar`, totalAmount, remarks, 1, NOW()
                        FROM tbl_budgetdrafts
                        WHERE departmentID = @department_id AND financialYearID = @financialYearID AND requestID IS NULL";

                        MySqlCommand insertCmd = new MySqlCommand(insertRequestQuery, connection, transaction);
                        insertCmd.Parameters.AddWithValue("@requestID", newRequestId);
                        insertCmd.Parameters.AddWithValue("@department_id", dto.departmentId);
                        insertCmd.Parameters.AddWithValue("@financialYearID", dto.financialYearId);
                        int insertedRows = insertCmd.ExecuteNonQuery();

                        // Update tbl_budgetdrafts with the new requestID
                        string updateDraftQuery = @"
                        UPDATE tbl_budgetdrafts 
                        SET requestID = @requestID, status = 'submitted' 
                        WHERE departmentID = @department_id AND financialYearID = @financialYearID AND requestID IS NULL";

                        MySqlCommand updateCmd = new MySqlCommand(updateDraftQuery, connection, transaction);
                        updateCmd.Parameters.AddWithValue("@requestID", newRequestId);
                        updateCmd.Parameters.AddWithValue("@department_id", dto.departmentId);
                        updateCmd.Parameters.AddWithValue("@financialYearID", dto.financialYearId);
                        int updatedRows = updateCmd.ExecuteNonQuery();

                        transaction.Commit();

                        return insertedRows > 0 && updatedRows > 0
                            ? Ok(new
                            {
                                Message = "Budget drafts submitted successfully.",
                                RequestId = newRequestId
                            })
                            : NotFound("No budget drafts found for submission.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(500, $"An error occurred: {ex.Message}");
                    }
                }
            }
        }

        [HttpPost("forward-request")]
        public IActionResult ForwardRequest([FromBody] ForwardRequestDto dto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verify current user is the request holder
                        string verifyQuery = @"
                SELECT COUNT(*) FROM tbl_request_actions 
                WHERE requestID = @requestId 
                AND requestedTo = @currentUser 
                AND status IN ('Pending', 'Reverted')"; // Allow reverting

                        MySqlCommand verifyCmd = new MySqlCommand(verifyQuery, connection, transaction);
                        verifyCmd.Parameters.AddWithValue("@requestId", dto.RequestId);
                        verifyCmd.Parameters.AddWithValue("@currentUser", dto.CurrentUser);

                        int validCount = Convert.ToInt32(verifyCmd.ExecuteScalar());
                        if (validCount == 0)
                        {
                            return BadRequest("You are not the current holder of this request or it's not in a forwardable state.");
                        }

                        // Determine the status to set
                        string newStatus = string.IsNullOrEmpty(dto.Status)
                            ? "Forwarded"
                            : dto.Status;

                        // Update request with new recipient and status
                        string updateQuery = @"
                UPDATE tbl_request_actions 
                SET requestedTo = @newRecipient, 
                    status = @status,
                    remark = @remarks
                WHERE requestID = @requestId";

                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection, transaction);
                        updateCmd.Parameters.AddWithValue("@requestId", dto.RequestId);
                        updateCmd.Parameters.AddWithValue("@newRecipient", dto.NewRecipient);
                        updateCmd.Parameters.AddWithValue("@status", newStatus);
                        updateCmd.Parameters.AddWithValue("@remarks", dto.Remarks);
                        updateCmd.ExecuteNonQuery();

                        // Add to history
                        string historyQuery = @"
                INSERT INTO request_history 
                (request_id, action, performed_by, performed_to, remarks) 
                VALUES 
                (@requestId, @action, @currentUser, @newRecipient, @remarks)";

                        MySqlCommand historyCmd = new MySqlCommand(historyQuery, connection, transaction);
                        historyCmd.Parameters.AddWithValue("@requestId", dto.RequestId);
                        historyCmd.Parameters.AddWithValue("@action", newStatus);
                        historyCmd.Parameters.AddWithValue("@currentUser", dto.CurrentUser);
                        historyCmd.Parameters.AddWithValue("@newRecipient", dto.NewRecipient);
                        historyCmd.Parameters.AddWithValue("@remarks", dto.Remarks);
                        historyCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return Ok(new { Message = "Request processed successfully." });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(500, $"An error occurred: {ex.Message}");
                    }
                }
            }
        }

        [HttpGet("request-history/{requestId}")]
        public IActionResult GetRequestHistory(int requestId)
        {
            List<RequestHistory> history = new List<RequestHistory>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = @"
                SELECT h.history_id, h.request_id, h.action, h.performed_by, 
                       h.performed_to, h.remarks, h.action_date,
                       u1.username AS performed_by_username,
                       u2.username AS performed_to_username
                FROM request_history h
                LEFT JOIN users u1 ON h.performed_by = u1.username
                LEFT JOIN users u2 ON h.performed_to = u2.username
                WHERE h.request_id = @requestId
                ORDER BY h.action_date DESC";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@requestId", requestId);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        history.Add(new RequestHistory
                        {
                            HistoryId = reader.GetInt32("history_id"),
                            RequestId = reader.GetInt32("request_id"),
                            Action = reader.GetString("action"),
                            PerformedBy = reader.GetString("performed_by_username"),
                            PerformedTo = reader.IsDBNull("performed_to_username") ? null : reader.GetString("performed_to_username"),
                            Remarks = reader.IsDBNull("remarks") ? null : reader.GetString("remarks"),
                            ActionDate = reader.GetDateTime("action_date")
                        });
                    }
                }
            }

            return Ok(history);
        }


    }
    }

