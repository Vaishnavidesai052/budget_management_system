namespace BudgetManagementSystemNew.Model;

public class DepartmentModel
{
    public int Id { get; set; }  // Corresponds to DepartmentID in DB
    public string Name { get; set; }
    public string Status { get; set; }  // Enum values: Active or Inactive
    public DateTime CreatedAt { get; set; }  // Timestamp for when the department was created
    public DateTime UpdatedAt { get; set; }  // Timestamp for when the department was last updated
}
