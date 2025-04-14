

//using System;
//using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using BudgetManagementSystemNew.Model;
//using Microsoft.Extensions.Configuration;

//namespace ExpenseManagementAPI.Repositories
//{
//    public class AddExpenseRepository
//    {
//        private readonly string _connectionString;

//        public AddExpenseRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection")
//                ?? throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
//        }

//        public AddExpenseRepository(string connectionString)
//        {
//            if (string.IsNullOrWhiteSpace(connectionString))
//            {
//                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
//            }

//            _connectionString = connectionString;
//        }

//        // Method to retrieve expenses for a specific year and month
//        public List<AddExpense> GetExpenses(int year, int month)
//        {
//            List<AddExpense> expenses = new List<AddExpense>();

//            using (MySqlConnection conn = new MySqlConnection(_connectionString))
//            {
//                string query = "SELECT Id, ItemName, ApprovedBudget, ActualExpense, Remarks, Timestamp FROM add_expense WHERE Year_ID = @Year AND Month_ID = @Month";

//                MySqlCommand cmd = new MySqlCommand(query, conn);
//                cmd.Parameters.AddWithValue("@Year", year);
//                cmd.Parameters.AddWithValue("@Month", month);

//                conn.Open();
//                using (MySqlDataReader reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        expenses.Add(new AddExpense
//                        {
//                            Id = reader.GetInt32(0),
//                            ItemName = reader.GetString(1),
//                            ApprovedBudget = reader.GetDecimal(2),
//                            ActualExpense = reader.GetDecimal(3),
//                            Remarks = reader.IsDBNull(4) ? null : reader.GetString(4),
//                            Timestamp = reader.GetDateTime(5)
//                        });
//                    }
//                }
//            }
//            return expenses;
//        }

//        // Method to update only the ActualExpense field
//        public bool UpdateActualExpense(int id, decimal actualExpense)
//        {
//            using (MySqlConnection conn = new MySqlConnection(_connectionString))
//            {
//                string query = "UPDATE add_expense SET ActualExpense = @ActualExpense WHERE Id = @Id";

//                MySqlCommand cmd = new MySqlCommand(query, conn);
//                cmd.Parameters.AddWithValue("@Id", id);
//                cmd.Parameters.AddWithValue("@ActualExpense", actualExpense);

//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                return rowsAffected > 0; // Returns true if the update was successful
//            }
//        }

//        // Optional: Method to update both ActualExpense and Remarks (if needed)
//        public bool UpdateExpense(int id, AddExpense updatedExpense)
//        {
//            using (MySqlConnection conn = new MySqlConnection(_connectionString))
//            {
//                string query = "UPDATE add_expense SET ActualExpense = @ActualExpense, Remarks = @Remarks WHERE Id = @Id";

//                MySqlCommand cmd = new MySqlCommand(query, conn);
//                cmd.Parameters.AddWithValue("@Id", id);
//                cmd.Parameters.AddWithValue("@ActualExpense", updatedExpense.ActualExpense);
//                cmd.Parameters.AddWithValue("@Remarks", updatedExpense.Remarks);

//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                return rowsAffected > 0; // Returns true if the update was successful
//            }
//        }
//    }
//}
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BudgetManagementSystemNew.Model;
using Microsoft.Extensions.Configuration;

namespace ExpenseManagementAPI.Repositories
{
    public class AddExpenseRepository
    {
        private readonly string _connectionString;

        public AddExpenseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
        }

        public AddExpenseRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        // Method to retrieve expenses for a specific year and month
        //public List<AddExpense> GetExpenses(int year, int month)
        //{
        //    List<AddExpense> expenses = new List<AddExpense>();

        //    using (MySqlConnection conn = new MySqlConnection(_connectionString))
        //    {
        //        string query = "SELECT Id, ItemName, ApprovedBudget, ActualExpense, Remarks, Timestamp FROM add_expense WHERE Year_ID = @Year AND Month_ID = @Month";

        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@Year", year);
        //        cmd.Parameters.AddWithValue("@Month", month);

        //        conn.Open();
        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                expenses.Add(new AddExpense
        //                {
        //                    Id = reader.GetInt32(0),
        //                    ItemName = reader.GetString(1),
        //                    ApprovedBudget = reader.GetDecimal(2),
        //                    ActualExpense = reader.GetDecimal(3),
        //                    Remarks = reader.IsDBNull(4) ? null : reader.GetString(4),
        //                    Timestamp = reader.GetDateTime(5)
        //                });
        //            }
        //        }
        //    }
        //    return expenses;
        //}
        public List<AddExpense> GetExpenses(int year, int month)
        {
            List<AddExpense> expenses = new List<AddExpense>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Id, ItemName, ApprovedBudget, ActualExpense, Remarks, Timestamp FROM add_expense WHERE Year_ID = @Year AND Month_ID = @Month";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                Console.WriteLine($"Query: {query}, Year: {year}, Month: {month}"); // Debugging

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(new AddExpense
                        {
                            Id = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            ApprovedBudget = reader.GetDecimal(2),
                            ActualExpense = reader.GetDecimal(3),
                            Remarks = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Timestamp = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return expenses;
        }

        // Method to update only the ActualExpense field
        public bool UpdateActualExpense(int id, decimal actualExpense)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE add_expense SET ActualExpense = @ActualExpense WHERE Id = @Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ActualExpense", actualExpense);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Returns true if the update was successful
            }
        }

        // Optional: Method to update both ActualExpense and Remarks (if needed)
        public bool UpdateExpense(int id, AddExpense updatedExpense)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE add_expense SET ActualExpense = @ActualExpense, Remarks = @Remarks WHERE Id = @Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@ActualExpense", updatedExpense.ActualExpense);
                cmd.Parameters.AddWithValue("@Remarks", updatedExpense.Remarks);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Returns true if the update was successful
            }
        }
    }
}