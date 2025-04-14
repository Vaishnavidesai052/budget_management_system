namespace BudgetManagementSystemNew.Models
{
    public class BudgetDraft
    {
        public int DraftID { get; set; }
        public int? RequestID { get; set; }
        public int DepartmentID { get; set; }

        public int CategoryID { get; set; }
        public int ItemID { get; set; }
        public int FinancialYearID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }  // ✅ Use "Dec_" in SQL to avoid conflicts
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
    }
}
