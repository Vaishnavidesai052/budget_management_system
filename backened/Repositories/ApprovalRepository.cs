
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

public class ApprovalRepository
{
    private readonly string _connectionString;

    public ApprovalRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Get all approvals with associated names and financial year
    // Get all approvals with associated names and financial year
    public List<ApprovalResponse> GetApprovals()
    {
        var approvals = new List<ApprovalResponse>();
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand(@"
            SELECT 
                a.Req_Id, 
                d.department_name, 
                u.username, 
                a.Budget_Estimation, 
                a.Date, 
                a.Year_Id, 
                s.statusName, 
                a.Remark
            FROM tbl_approval a
            INNER JOIN tbl_department d ON a.Dept_Id = d.department_id
            INNER JOIN tbl_user u ON a.User_Id = u.User_Id
            INNER JOIN tbl_status s ON a.Status_Id = s.statusID", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get Year using Year_Id
                        int? yearId = reader.IsDBNull("Year_Id") ? null : (int?)reader.GetInt32("Year_Id");
                        string year = GetYearById(yearId); // Fetch the financial year

                        approvals.Add(new ApprovalResponse
                        {
                            Req_Id = reader.GetInt32("Req_Id"),
                            Dept_Name = reader.GetString("department_name"),
                            User_Name = reader.GetString("username"),
                            Budget_Estimation = reader.IsDBNull("Budget_Estimation") ? null : (uint?)reader.GetUInt32("Budget_Estimation"),
                            Date = reader.GetDateTime("Date"),
                            Year = year,  // Set the financial year here (as string)
                            Status_Name = reader.GetString("statusName"),
                            Remark = reader.IsDBNull("Remark") ? null : reader.GetString("Remark"),
                            Year_Id = yearId
                        });
                    }
                }
            }
        }
        return approvals;
    }

    // Get approval by ID with associated names and financial year
    public ApprovalResponse GetApprovalById(int reqId)
    {
        ApprovalResponse approval = null;
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand(@"
            SELECT 
                a.Req_Id, 
                d.deptartment_name, 
                u.username, 
                a.Budget_Estimation, 
                a.Date, 
                a.Year_Id, 
                s.statusName, 
                a.Remark
            FROM tbl_approval a
            INNER JOIN tbl_department d ON a.Dept_Id = d.department_id
            INNER JOIN tbl_user u ON a.User_Id = u.User_Id
            INNER JOIN tbl_status s ON a.Status_Id = s.Status_Id
            WHERE a.Req_Id = @Req_Id", conn))
            {
                cmd.Parameters.AddWithValue("@Req_Id", reqId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Get Year using Year_Id
                        int? yearId = reader.IsDBNull("Year_Id") ? null : (int?)reader.GetInt32("Year_Id");
                        string year = GetYearById(yearId); // Fetch the financial year

                        approval = new ApprovalResponse
                        {
                            Req_Id = reader.GetInt32("Req_Id"),
                            Dept_Name = reader.GetString("department_name"),
                            User_Name = reader.GetString("username"),
                            Budget_Estimation = reader.IsDBNull("Budget_Estimation") ? null : (uint?)reader.GetUInt32("Budget_Estimation"),
                            Date = reader.GetDateTime("Date"),
                            Year = year,  // Set the financial year here (as string)
                            Status_Name = reader.GetString("statusName"),
                            Remark = reader.IsDBNull("Remark") ? null : reader.GetString("Remark"),
                            Year_Id = yearId
                        };
                    }
                }
            }
        }
        return approval;
    }


    // Create a new approval
    public void CreateApproval(Approval approval)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand(@"
                INSERT INTO tbl_approval 
                    (Dept_Id, User_Id, Budget_Estimation, Date, Year_Id, Status_Id, Remark) 
                VALUES 
                    (@Dept_Id, @User_Id, @Budget_Estimation, @Date, @Year_Id, @Status_Id, @Remark)", conn))
            {
                cmd.Parameters.AddWithValue("@Dept_Id", approval.Dept_Id);
                cmd.Parameters.AddWithValue("@User_Id", approval.User_Id);
                cmd.Parameters.AddWithValue("@Budget_Estimation", approval.Budget_Estimation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", approval.Date);
                cmd.Parameters.AddWithValue("@Year_Id", approval.Year_Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status_Id", approval.Status_Id);
                cmd.Parameters.AddWithValue("@Remark", approval.Remark ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }
    }

    // Update an existing approval
    public void UpdateApproval(Approval approval)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand(@"
                UPDATE tbl_approval 
                SET 
                    Dept_Id = @Dept_Id, 
                    User_Id = @User_Id, 
                    Budget_Estimation = @Budget_Estimation, 
                    Date = @Date, 
                    Year_Id = @Year_Id, 
                    Status_Id = @Status_Id, 
                    Remark = @Remark 
                WHERE Req_Id = @Req_Id", conn))
            {
                cmd.Parameters.AddWithValue("@Req_Id", approval.Req_Id);
                cmd.Parameters.AddWithValue("@Dept_Id", approval.Dept_Id);
                cmd.Parameters.AddWithValue("@User_Id", approval.User_Id);
                cmd.Parameters.AddWithValue("@Budget_Estimation", approval.Budget_Estimation ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", approval.Date);
                cmd.Parameters.AddWithValue("@Year_Id", approval.Year_Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status_Id", approval.Status_Id);
                cmd.Parameters.AddWithValue("@Remark", approval.Remark ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }
    }

    // Delete an approval
    public void DeleteApproval(int reqId)
    {
        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM tbl_approval WHERE Req_Id = @Req_Id", conn))
            {
                cmd.Parameters.AddWithValue("@Req_Id", reqId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Fetch the financial year by Year_Id
    public string GetYearById(int? yearId)
    {
        string financialYear = null;
        if (yearId.HasValue)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT financial_year FROM tbl_year WHERE id = @Year_Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Year_Id", yearId.Value);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            financialYear = reader.IsDBNull("financial_year") ? null : reader.GetString("financial_year");
                        }
                    }
                }
            }
        }
        return financialYear;
    }
}
