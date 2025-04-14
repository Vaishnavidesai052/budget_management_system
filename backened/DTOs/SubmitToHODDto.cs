namespace BudgetManagementSystemNew.DTOs
{
    public class SubmitToHODDto
    {
        public int departmentId { get; set; }
        public int financialYearId { get; set; }
        public string requestedBy { get; set; } // New property for requestedBy
        public string requestedTo { get; set; } // New property for requestedTo
    }
}
