namespace BudgetManagementSystemNew.Model
{
    public class RevertedRemark
    {

        public int RevertID { get; set; } // Primary key, auto-incremented
        public string RequestID { get; set; } // Foreign key to tbl_request_actions
        public int? ItemID { get; set; } // Foreign key to tbl_items (nullable for common remarks)
        public int FinancialYearID { get; set; } // Foreign key to financial year
        public string RevertRemarks { get; set; } // Remarks for the reverted request
        public string RevertBy { get; set; } // User who reverted the request
        public DateTime RevertAt { get; set; } // Timestamp of the revert action
    }
}
