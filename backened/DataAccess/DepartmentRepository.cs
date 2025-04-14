using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using BudgetManagementSystemNew.Model;

namespace BudgetManagementSystemNew.Repositories;

public class DepartmentRepository
{
    private readonly string _connectionString;

    public DepartmentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Get all departments
    public IEnumerable<DepartmentModel> GetAllDepartments()
    {
        var departments = new List<DepartmentModel>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Department", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            departments.Add(new DepartmentModel
            {
                Id = Convert.ToInt32(reader["DepartmentID"]),
                Name = reader["Name"].ToString(),
                Status = reader["Status"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            });
        }

        return departments;
    }

    // Get department by ID
    public DepartmentModel GetDepartmentById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("SELECT * FROM Department WHERE DepartmentID = @DepartmentID", connection);
        command.Parameters.AddWithValue("@DepartmentID", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new DepartmentModel
            {
                Id = Convert.ToInt32(reader["DepartmentID"]),
                Name = reader["Name"].ToString(),
                Status = reader["Status"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
            };
        }

        return null;
    }

    // Add a new department
    public void AddDepartment(DepartmentModel department)
    {
        if (department.Status != "Active" && department.Status != "Inactive")
        {
            throw new InvalidOperationException("Invalid Status value. It must be 'Active' or 'Inactive'.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "INSERT INTO Department (Name, Status, CreatedAt, UpdatedAt) VALUES (@Name, @Status, @CreatedAt, @UpdatedAt)", connection);
        command.Parameters.AddWithValue("@Name", department.Name);
        command.Parameters.AddWithValue("@Status", department.Status);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);  // Set current timestamp for CreatedAt
        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);  // Set current timestamp for UpdatedAt

        command.ExecuteNonQuery();
    }

    // Update an existing department
    public void UpdateDepartment(int id, DepartmentModel department)
    {
        if (department.Status != "Active" && department.Status != "Inactive")
        {
            throw new InvalidOperationException("Invalid Status value. It must be 'Active' or 'Inactive'.");
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand(
            "UPDATE Department SET Name = @Name, Status = @Status, UpdatedAt = @UpdatedAt WHERE DepartmentID = @DepartmentID", connection);
        command.Parameters.AddWithValue("@DepartmentID", id);
        command.Parameters.AddWithValue("@Name", department.Name);
        command.Parameters.AddWithValue("@Status", department.Status);
        command.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);  // Set current timestamp for UpdatedAt

        command.ExecuteNonQuery();
    }

    // Delete a department
    public void DeleteDepartment(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        var command = new MySqlCommand("DELETE FROM Department WHERE DepartmentID = @DepartmentID", connection);
        command.Parameters.AddWithValue("@DepartmentID", id);

        command.ExecuteNonQuery();
    }
}
