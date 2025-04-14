namespace BudgetManagementSystemNew.Model
{
    public class RequestAction
    {

        public int RequestID { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedTo { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? RevertedAt { get; set; }
    }
}
