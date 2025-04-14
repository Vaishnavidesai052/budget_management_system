using BudgetManagementSystemNew.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestActionController : ControllerBase
    {
        private readonly string _connectionString;
        private object financialYearID;
        private object requestedBy;

        public RequestActionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult<IEnumerable<RequestAction>> GetRequestActions()
        {
            var requestActions = new List<RequestAction>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tbl_request_actions";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestActions.Add(new RequestAction
                        {
                            RequestID = reader.GetInt32("requestID"),
                            RequestedBy = reader.GetString("requestedBy"),
                            RequestedTo = reader.GetString("requestedTo"),
                            Status = reader.GetString("status"),
                            Remark = reader["remark"].ToString(),
                            RequestedAt = reader.GetDateTime("requestedAt"),
                            ApprovedAt = reader.IsDBNull(reader.GetOrdinal("approvedAt")) ? (DateTime?)null : reader.GetDateTime("approvedAt"),
                            RejectedAt = reader.IsDBNull(reader.GetOrdinal("rejectedAt")) ? (DateTime?)null : reader.GetDateTime("rejectedAt"),
                            RevertedAt = reader.IsDBNull(reader.GetOrdinal("revertedAt")) ? (DateTime?)null : reader.GetDateTime("revertedAt")
                        });
                    }
                }
            }

            return requestActions.Count == 0 ? NotFound() : Ok(requestActions);
        }


        [HttpPatch("{id}/revert")]
        public ActionResult RevertRequestAction(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Check if the request exists and is in a revertible state
                        var checkQuery = "SELECT status FROM tbl_request_actions WHERE requestID = @RequestID";
                        using (var checkCommand = new MySqlCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@RequestID", id);
                            using (var reader = checkCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    return NotFound("Request not found.");
                                }

                                var status = reader["status"].ToString();
                                if (status != "Approved" && status != "Pending")
                                {
                                    return BadRequest($"Request cannot be reverted because it is in the '{status}' state.");
                                }
                            }
                        }

                        // Step 2: Update the request status to 'Reverted'
                        var updateQuery = "UPDATE tbl_request_actions SET status = 'Reverted', revertedAt = @RevertedAt WHERE requestID = @RequestID";
                        using (var updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@RequestID", id);
                            updateCommand.Parameters.AddWithValue("@RevertedAt", DateTime.UtcNow);
                            var rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return StatusCode(500, "Failed to revert the request.");
                            }
                        }

                        transaction.Commit();
                        return Ok(new { Message = "Request successfully reverted.", Status = "Reverted" });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(500, $"An error occurred: {ex.Message}");
                    }
                }
            }
        }

        [HttpPost("{id}/remarks")]
        public ActionResult SaveRemarks(int id, [FromBody] RemarkRequest remarkRequest)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Insert Common Remark into tbl_revert_remarks
                        if (!string.IsNullOrEmpty(remarkRequest.CommonRemark))
                        {
                            var insertCommonRemarkQuery = @"INSERT INTO tbl_revert_remarks 
                        (requestID, itemID, revertRemarks, revertBy, revertAt) 
                        VALUES (@RequestID, NULL, @RevertRemarks, @RevertBy, @RevertAt)";

                            using (var insertCommand = new MySqlCommand(insertCommonRemarkQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@RequestID", id);
                                insertCommand.Parameters.AddWithValue("@RevertRemarks", remarkRequest.CommonRemark);
                                insertCommand.Parameters.AddWithValue("@RevertBy", "System"); // Change to actual user
                                insertCommand.Parameters.AddWithValue("@RevertAt", DateTime.UtcNow);
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        // Step 2: Insert Item-Specific Remarks into tbl_item_remarks
                        var insertItemRemarkQuery = @"INSERT INTO tbl_item_remarks 
                    (requestID, itemID, remark, remarkBy, remarkAt) 
                    VALUES (@RequestID, @ItemID, @ItemRemark, @RemarkBy, @RemarkAt)";

                        foreach (var itemRemark in remarkRequest.ItemRemarks)
                        {
                            if (!string.IsNullOrEmpty(itemRemark.Remark))
                            {
                                using (var insertCommand = new MySqlCommand(insertItemRemarkQuery, connection, transaction))
                                {
                                    insertCommand.Parameters.AddWithValue("@RequestID", id);
                                    insertCommand.Parameters.AddWithValue("@ItemID", itemRemark.ItemID);
                                    insertCommand.Parameters.AddWithValue("@ItemRemark", itemRemark.Remark);
                                    insertCommand.Parameters.AddWithValue("@RemarkBy", "System");
                                    insertCommand.Parameters.AddWithValue("@RemarkAt", DateTime.UtcNow);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        return Ok("Remarks saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(500, $"An error occurred: {ex.Message}");
                    }
                }
            }
        }
        [HttpPost("approve/{requestId}")]
        public ActionResult ApproveBudgetRequest(int requestId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Step 1: Check if the request exists
                var checkQuery = "SELECT requestID FROM tbl_request_actions WHERE requestID = @RequestID";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@RequestID", requestId);
                    using (var reader = checkCommand.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return NotFound("Budget request not found.");
                        }
                    }
                }

                // Step 2: Update the budget request status to 'Approved'
                var updateQuery = @"UPDATE tbl_request_actions
                            SET status = 'Approved', approvedAt = @ApprovedAt
                            WHERE requestID = @RequestID";
                using (var updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@RequestID", requestId);
                    updateCommand.Parameters.AddWithValue("@ApprovedAt", DateTime.UtcNow);
                    var rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return StatusCode(500, "Failed to approve the budget request.");
                    }
                }
            }

            return Ok(new { Message = "Budget request approved successfully." });
        }
        [HttpGet("item-remarks/all")]
        public ActionResult<IEnumerable<object>> GetAllItemRemarks()
        {
            var remarksList = new List<object>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT requestID, itemID, remark, remarkBy, remarkAt 
                      FROM tbl_item_remarks";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        remarksList.Add(new
                        {
                            RequestID = reader["requestID"],
                            ItemID = reader["itemID"],
                            Remark = reader["remark"].ToString(),
                            RemarkBy = reader["remarkBy"].ToString(),
                            RemarkAt = Convert.ToDateTime(reader["remarkAt"])
                        });
                    }
                }
            }

            return Ok(remarksList);
        }
        [HttpGet("reverted-items/{requestId}")]
        public ActionResult<IEnumerable<object>> GetRevertedItems(int requestId)
        {
            var revertedItemsList = new List<object>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            SELECT tir.requestID, tir.itemID, tir.remark, tir.remarkBy, tir.remarkAt 
            FROM tbl_item_remarks tir
            JOIN tbl_request_actions tra ON tir.requestID = tra.requestID
            WHERE tir.requestID = @RequestID AND tra.status = 'Reverted'";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            revertedItemsList.Add(new
                            {
                                RequestID = reader["requestID"],
                                ItemID = reader["itemID"],
                                Remark = reader["remark"].ToString(),
                                RemarkBy = reader["remarkBy"].ToString(),
                                RemarkAt = Convert.ToDateTime(reader["remarkAt"])
                            });
                        }
                    }
                }
            }

            if (revertedItemsList.Count == 0)
            {
                return NotFound("No reverted items found for this request.");
            }

            return Ok(revertedItemsList);
        }

        [HttpGet("remarks/all")]
        public ActionResult<IEnumerable<object>> GetAllRevertedRemarks()
        {
            var remarksList = new List<object>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT requestID, itemID, revertRemarks, revertBy, revertAt 
                      FROM tbl_revert_remarks";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        remarksList.Add(new
                        {
                            RequestID = reader["requestID"],
                            ItemID = reader["itemID"] != DBNull.Value ? reader["itemID"] : null,
                            RevertRemarks = reader["revertRemarks"].ToString(),
                            RevertBy = reader["revertBy"].ToString(),
                            RevertAt = Convert.ToDateTime(reader["revertAt"])
                        });
                    }
                }
            }

            return Ok(remarksList);
        }

        [HttpGet("remarks/{requestId}")]
        public ActionResult<IEnumerable<object>> GetRevertedRemarksByRequestId(int requestId)
        {
            var remarksList = new List<object>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT requestID, itemID, revertRemarks, revertBy, revertAt 
                      FROM tbl_revert_remarks
                      WHERE requestID = @RequestID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            remarksList.Add(new
                            {
                                RequestID = reader["requestID"],
                                ItemID = reader["itemID"] != DBNull.Value ? reader["itemID"] : null,
                                RevertRemarks = reader["revertRemarks"].ToString(),
                                RevertBy = reader["revertBy"].ToString(),
                                RevertAt = Convert.ToDateTime(reader["revertAt"])
                            });
                        }
                    }
                }
            }

            if (remarksList.Count == 0)
            {
                return NotFound("No reverted remarks found for this request.");
            }

            return Ok(remarksList);
        }


        [HttpGet("{id}")]
        public ActionResult<RequestAction> GetRequestActionById(int id)
        {
            RequestAction requestAction = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM tbl_request_actions WHERE requestID = @RequestID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            requestAction = new RequestAction
                            {
                                RequestID = reader.GetInt32("requestID"),
                                RequestedBy = reader.GetString("requestedBy"),
                                RequestedTo = reader.GetString("requestedTo"),
                                Status = reader.GetString("status"),
                                Remark = reader["remark"].ToString(),
                                RequestedAt = reader.GetDateTime("requestedAt"),
                                ApprovedAt = reader.IsDBNull(reader.GetOrdinal("approvedAt")) ? (DateTime?)null : reader.GetDateTime("approvedAt"),
                                RejectedAt = reader.IsDBNull(reader.GetOrdinal("rejectedAt")) ? (DateTime?)null : reader.GetDateTime("rejectedAt"),
                                RevertedAt = reader.IsDBNull(reader.GetOrdinal("revertedAt")) ? (DateTime?)null : reader.GetDateTime("revertedAt")
                            };
                        }
                    }
                }
            }

            return requestAction == null ? NotFound() : Ok(requestAction);
        }

        [HttpPost]
        public ActionResult<RequestAction> PostRequestAction(RequestAction requestAction)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"INSERT INTO tbl_request_actions 
                              (requestedBy, requestedTo, status, remark, requestedAt, approvedAt, rejectedAt, revertedAt)
                              VALUES 
                              (@RequestedBy, @RequestedTo, @Status, @Remark, @RequestedAt, @ApprovedAt, @RejectedAt, @RevertedAt)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestedBy", requestAction.RequestedBy);
                    command.Parameters.AddWithValue("@RequestedTo", requestAction.RequestedTo);
                    command.Parameters.AddWithValue("@Status", requestAction.Status);
                    command.Parameters.AddWithValue("@Remark", requestAction.Remark);
                    command.Parameters.AddWithValue("@RequestedAt", requestAction.RequestedAt);
                    command.Parameters.AddWithValue("@ApprovedAt", (object)requestAction.ApprovedAt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@RejectedAt", (object)requestAction.RejectedAt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@RevertedAt", (object)requestAction.RevertedAt ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }

            return CreatedAtAction(nameof(GetRequestActions), new { id = requestAction.RequestID }, requestAction);
        }



        [HttpPut("{id}")]
        public ActionResult UpdateRequestAction(int id, RequestAction requestAction)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"UPDATE tbl_request_actions
                              SET requestedBy = @RequestedBy,
                                  requestedTo = @RequestedTo,
                                  status = @Status,
                                  remark = @Remark,
                                  requestedAt = @RequestedAt,
                                  approvedAt = @ApprovedAt,
                                  rejectedAt = @RejectedAt,
                                  revertedAt = @RevertedAt
                              WHERE requestID = @RequestID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", id);
                    command.Parameters.AddWithValue("@RequestedBy", requestAction.RequestedBy);
                    command.Parameters.AddWithValue("@RequestedTo", requestAction.RequestedTo);
                    command.Parameters.AddWithValue("@Status", requestAction.Status);
                    command.Parameters.AddWithValue("@Remark", requestAction.Remark);
                    command.Parameters.AddWithValue("@RequestedAt", requestAction.RequestedAt);
                    command.Parameters.AddWithValue("@ApprovedAt", (object)requestAction.ApprovedAt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@RejectedAt", (object)requestAction.RejectedAt ?? DBNull.Value);
                    command.Parameters.AddWithValue("@RevertedAt", (object)requestAction.RevertedAt ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRequestAction(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM tbl_request_actions WHERE requestID = @RequestID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", id);
                    command.ExecuteNonQuery();
                }
            }

            return NoContent();
        }
    }
}