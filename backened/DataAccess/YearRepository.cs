using BudgetManagementSystemNew.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BudgetManagementSystemNew.DataAccess

{
    public class YearRepository
    {
        private readonly string _connectionString;

        public YearRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Year> GetAllYears()
        {
            var years = new List<Year>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Year_ID, Fiscal_Year FROM Years";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            years.Add(new Year
                            {
                                Year_ID = reader.GetInt32("Year_ID"),
                                Fiscal_Year = reader.GetString("Fiscal_Year")
                            });
                        }
                    }
                }
            }
            return years;
        }
    }
}