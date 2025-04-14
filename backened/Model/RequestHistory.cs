namespace BudgetManagementSystemNew.Model
{
    public class RequestHistory
    {
        public int HistoryId { get; set; }
        public int RequestId { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedTo { get; set; }
        public string Remarks { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
