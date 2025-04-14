
using System.Collections.Generic;
using BudgetManagementSystemNew.Model;
using MySql.Data.MySqlClient;

namespace BudgetManagementSystemNew.DataAccess
{
    public class ViewExpenseRepository
    {
        private readonly string _connectionString;

        public ViewExpenseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ViewExpenseModel> GetViewExpenses(int yearId)
        {
            var expenses = new List<ViewExpenseModel>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT Id, ItemName, ApprovedBudget, ActualExpense, Month_ID, Year_ID, Remarks, Timestamp
                    FROM add_expense
                    WHERE Year_ID = @Year_ID";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Year_ID", yearId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(new ViewExpenseModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ItemName = reader.IsDBNull(reader.GetOrdinal("ItemName")) ? "" : reader.GetString(reader.GetOrdinal("ItemName")),
                                ApprovedBudget = reader.IsDBNull(reader.GetOrdinal("ApprovedBudget")) ? 0.00m : reader.GetDecimal(reader.GetOrdinal("ApprovedBudget")),
                                ActualExpense = reader.IsDBNull(reader.GetOrdinal("ActualExpense")) ? 0.00m : reader.GetDecimal(reader.GetOrdinal("ActualExpense")),
                                Month_ID = reader.GetInt32(reader.GetOrdinal("Month_ID")),
                                Year_ID = reader.GetInt32(reader.GetOrdinal("Year_ID")),
                                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? "" : reader.GetString(reader.GetOrdinal("Remarks")),
                                Timestamp = reader.IsDBNull(reader.GetOrdinal("Timestamp")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Timestamp"))
                            });
                        }

                    }
                }
            }

            return expenses;
        }
    }
}