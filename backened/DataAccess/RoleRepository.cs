using MySql.Data.MySqlClient;
using SecureAuthApi.Models;
using System;
using System.Collections.Generic;

public class RoleRepository
{
    private readonly string _connectionString;

    public RoleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Get all roles
    public IEnumerable<Role> GetAllRoles()
    {
        var roles = new List<Role>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Role", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            roles.Add(new Role
            {
                RoleID = Convert.ToInt32(reader["RoleID"]),
                RoleName = reader["RoleName"].ToString(),
                Status = reader["Status"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            });
        }

        return roles;
    }

    // Get role by ID
    public Role GetRoleById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Role WHERE RoleID = @RoleID", connection);
        command.Parameters.AddWithValue("@RoleID", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Role
            {
                RoleID = Convert.ToInt32(reader["RoleID"]),
                RoleName = reader["RoleName"].ToString(),
                Status = reader["Status"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            };
        }

        return null;
    }

    // Add a new role
    public void AddRole(Role role)
    {
        if (role.Status != "Active" && role.Status != "Inactive")
        {
            throw new InvalidOperationException("Invalid Status value. It must be 'Active' or 'Inactive'.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "INSERT INTO Role (RoleName, Status, CreatedAt, UpdatedAt) VALUES (@RoleName, @Status, @CreatedAt, @UpdatedAt)", connection);

        command.Parameters.AddWithValue("@RoleName", role.RoleName);
        command.Parameters.AddWithValue("@Status", role.Status);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);  // Set CreatedAt as the current UTC time
        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);  // Set UpdatedAt as the current UTC time

        command.ExecuteNonQuery();
    }

    // Update a role
    public void UpdateRole(int id, Role role)
    {
        if (role.Status != "Active" && role.Status != "Inactive")
        {
            throw new InvalidOperationException("Invalid Status value. It must be 'Active' or 'Inactive'.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "UPDATE Role SET RoleName = @RoleName, Status = @Status, UpdatedAt = @UpdatedAt WHERE RoleID = @RoleID", connection);

        command.Parameters.AddWithValue("@RoleID", id);
        command.Parameters.AddWithValue("@RoleName", role.RoleName);
        command.Parameters.AddWithValue("@Status", role.Status);
        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);  // Update UpdatedAt to the current UTC time

        command.ExecuteNonQuery();
    }

    // Delete a role
    public void DeleteRole(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("DELETE FROM Role WHERE RoleID = @RoleID", connection);
        command.Parameters.AddWithValue("@RoleID", id);

        command.ExecuteNonQuery();
    }
}
