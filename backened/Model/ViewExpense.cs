namespace BudgetManagementSystemNew.Model
{
    public class ViewExpenseModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal ApprovedBudget { get; set; }
        public decimal ActualExpense { get; set; }
        public int Month_ID { get; set; }
        public int Year_ID { get; set; }
        public string Remarks { get; set; }
        public DateTime Timestamp { get; set; }
    }
}