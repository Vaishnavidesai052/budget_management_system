namespace BudgetManagementSystemNew.Model
{
    public class Employee
    {
        public int EmpId { get; set; }
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public int? EmpDept { get; set; }
        public string EmpDesignation { get; set; }
        public string EmpPost { get; set; }
        public string? EmpEmail { get; set; }
        public int? EmpRole { get; set; }
        public string? EmpPassword { get; set; }
        public string? EmpStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
