namespace BudgetManagementSystemNew.Model
{
    public class RevertResponse
    {

        public string Status { get; set; }
        public string CommonRemarks { get; set; }
        public string RevertBy { get; set; } // User who requested the revert
        public List<ItemRemark> ItemRemarks { get; set; }

        public RevertResponse()
        {
            Status = "Reverted";
            CommonRemarks = string.Empty;
            RevertBy = string.Empty;
            ItemRemarks = new List<ItemRemark>();
        }
    }
}
