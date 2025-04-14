

using MySql.Data.MySqlClient;
using SecureAuthApi.Models;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Check if the user already exists by email
    public bool UserExists(string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection);
        command.Parameters.AddWithValue("@Email", email);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    // Check if the email is taken by another user (excluding the current user)
    public bool IsEmailTaken(string email, int userId)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "SELECT COUNT(*) FROM Users WHERE Email = @Email AND UserID != @UserID", connection);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@UserID", userId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    // Check if the user exists by ID
    public bool UserExistsById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT COUNT(*) FROM Users WHERE UserID = @UserID", connection);
        command.Parameters.AddWithValue("@UserID", id);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    // Get the password hash for a user by email
    public string GetPasswordHash(string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT PasswordHash FROM Users WHERE Email = @Email", connection);
        command.Parameters.AddWithValue("@Email", email);

        return command.ExecuteScalar()?.ToString();
    }

    // Add a new user to the database
    public void AddUser(RegisterModel model)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var passwordHash = HashPassword(model.Password);

        var command = new MySqlCommand(
            "INSERT INTO Users (Email, PasswordHash, Username, DepartmentID, RoleID, Status) " +
            "VALUES (@Email, @PasswordHash, @Username, @DepartmentID, @RoleID, 'Active')",
            connection);

        command.Parameters.AddWithValue("@Email", model.Email);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
        command.Parameters.AddWithValue("@Username", model.Username);
        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentId);
        command.Parameters.AddWithValue("@RoleID", model.RoleId);

        command.ExecuteNonQuery();
    }

    // Get all users
    public IEnumerable<User> GetAllUsers()
    {
        var users = new List<User>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Users", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            users.Add(new User
            {
                Id = Convert.ToInt32(reader["UserID"]),  // Use UserID instead of Id
                Email = reader["Email"].ToString(),
                Username = reader["Username"].ToString(),
                Departmentid = reader["DepartmentID"]?.ToString(),
                RoleId = reader["RoleID"] != DBNull.Value ? Convert.ToInt32(reader["RoleID"]) : (int?)null , // ✅ Fetch RoleID Only
                PasswordHash = null // Exclude password hash from retrieval
            });
        }

        return users;
    }

    // Get user by ID
    //public User GetUserById(int id)
    //{
    //    using var connection = new MySqlConnection(_connectionString);
    //    connection.Open();

    //    var command = new MySqlCommand("SELECT * FROM Users WHERE UserID = @UserID", connection);  // Use UserID instead of Id
    //    command.Parameters.AddWithValue("@UserID", id);  // Parameter name should be UserID

    //    using var reader = command.ExecuteReader();

    //    if (reader.Read())
    //    {
    //        return new User
    //        {
    //            Id = Convert.ToInt32(reader["UserID"]),  // Use UserID here as well
    //            Email = reader["Email"].ToString(),
    //            Username = reader["Username"].ToString(),
    //            Departmentid = reader["DepartmentID"]?.ToString(),
    //            PasswordHash = null // Exclude password hash from retrieval
    //        };
    //    }

    //    return null;
    //}

    //public User GetUserById(int id)
    //{
    //    using var connection = new MySqlConnection(_connectionString);
    //    connection.Open();

    //    var command = new MySqlCommand(@"
    //    SELECT 
    //        u.UserID, 
    //        u.Username, 
    //        u.Email, 
    //        u.DepartmentID, 
    //        u.RoleID  
    //    FROM Users u
    //    WHERE u.UserID = @UserID", connection);  // ✅ Fixed WHERE clause

    //    command.Parameters.AddWithValue("@UserID", id);

    //    using var reader = command.ExecuteReader();

    //    if (reader.Read())
    //    {
    //        return new User
    //        {
    //            Id = Convert.ToInt32(reader["UserID"]),
    //            Email = reader["Email"].ToString(),
    //            Username = reader["Username"].ToString(),
    //            Departmentid = reader["DepartmentID"]?.ToString(),
    //            RoleId = reader["RoleID"] != DBNull.Value ? Convert.ToInt32(reader["RoleID"]) : (int?)null  // ✅ Fetch RoleID Only
    //        };
    //    }

    //    return null;
    //}

    public User GetUserById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(@"
        SELECT 
            u.UserID, 
            u.Username, 
            u.Email, 
            u.DepartmentID, 
            u.RoleID, 
            (SELECT r.RoleName FROM Role r WHERE r.RoleID = u.RoleID) AS RoleName -- ✅ Fetch role_Name dynamically
        FROM Users u
        WHERE u.UserID = @UserID", connection);

        command.Parameters.AddWithValue("@UserID", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["UserID"]),
                Email = reader["Email"].ToString(),
                Username = reader["Username"].ToString(),
                Departmentid = reader["DepartmentID"]?.ToString(),
                RoleId = reader["RoleID"] != DBNull.Value ? Convert.ToInt32(reader["RoleID"]) : (int?)null,
                Role_Name = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : null // ✅ Fetch role_Name
            };
        }

        return null;
    }


    // Update user
    public void UpdateUser(int id, UserUpdateModel model)
    {
        // Check if the email is already taken by another user (excluding the current user)
        if (IsEmailTaken(model.Email, id))
        {
            throw new InvalidOperationException("This email is already in use by another user.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "UPDATE Users SET Username = @Username, Email = @Email, DepartmentID = @DepartmentID, RoleID = @RoleID, Status = @Status " +
            "WHERE UserID = @UserID", connection);  // Use UserID instead of Id

        // Add parameters from the UserUpdateModel
        command.Parameters.AddWithValue("@UserID", id);  // Parameter name should be UserID
        command.Parameters.AddWithValue("@Username", model.Username);
        command.Parameters.AddWithValue("@Email", model.Email);
        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentId);
        command.Parameters.AddWithValue("@RoleID", model.RoleId);
        command.Parameters.AddWithValue("@Status", model.Status);

        // Execute the update command
        command.ExecuteNonQuery();
    }

    // Delete user
    public void DeleteUser(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("DELETE FROM Users WHERE UserID = @UserID", connection);  // Use UserID here
        command.Parameters.AddWithValue("@UserID", id);  // Use the correct parameter for UserID

        // Execute the delete command
        command.ExecuteNonQuery();
    }

    public void SavePasswordResetToken(int userId, string resetToken)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "UPDATE Users SET ResetToken = @ResetToken WHERE UserID = @UserID", connection);
        command.Parameters.AddWithValue("@ResetToken", resetToken);
        command.Parameters.AddWithValue("@UserID", userId);

        command.ExecuteNonQuery();
    }

    // Get user by email (for password reset)
    public User GetUserByEmail(string email)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Users WHERE Email = @Email", connection);
        command.Parameters.AddWithValue("@Email", email);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["UserID"]),
                Email = reader["Email"].ToString(),
                Username = reader["Username"].ToString(),
                Departmentid = reader["DepartmentID"].ToString(),
                 RoleId = reader["RoleId"] as int? // Explicitly handling null

                // other properties
            };
        }

        return null;
    }

    public User GetUserByResetToken(string resetToken)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Users WHERE ResetToken = @ResetToken", connection);
        command.Parameters.AddWithValue("@ResetToken", resetToken);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = Convert.ToInt32(reader["UserID"]), // Adjust field name if necessary
                Email = reader["Email"].ToString(),
                Username = reader["Username"].ToString(),
                Departmentid = reader["DepartmentID"]?.ToString(),
                PasswordHash = null, // We don't need to return the password hash
                ResetToken = reader["ResetToken"]?.ToString(),
                ResetTokenExpiration = reader["ResetTokenExpiration"] as DateTime? // assuming the token expiration field exists
            };
        }

        return null;
    }

    // Helper method to hash password
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
