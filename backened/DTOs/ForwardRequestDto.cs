namespace BudgetManagementSystemNew.DTOs
{
    public class ForwardRequestDto
    {
        public int RequestId { get; set; }
        public string CurrentUser { get; set; }
        public string NewRecipient { get; set; }
        public string Remarks { get; set; }

        public string Status { get; set; } // Add this property
    }
}
