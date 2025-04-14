namespace BudgetManagementSystemNew.Model
{
    public class Year
    {
        public int Year_ID { get; set; }
        public string Fiscal_Year { get; set; }
        public DateTime CreatedAt { get; set; } // New column
        public DateTime UpdatedAt { get; set; } // New column
    }
}