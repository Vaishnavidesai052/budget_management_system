

public class ApprovalResponse
{
    public int Req_Id { get; set; }
    public string Dept_Name { get; set; }
    public string User_Name { get; set; }
    public uint? Budget_Estimation { get; set; }
    public DateTime Date { get; set; }
    public string Year { get; set; }  // Year is now a string (financial_year)
    public string Status_Name { get; set; }
    public string? Remark { get; set; }
    public int? Year_Id { get; set; }
}

public class Approval
{
    public int Req_Id { get; set; }
    public int Dept_Id { get; set; }
    public int User_Id { get; set; }
    public uint? Budget_Estimation { get; set; }
    public DateTime Date { get; set; }
    public int Year { get; set; }
    public int Status_Id { get; set; }
    public string? Remark { get; set; }
    public int? Year_Id { get; set; }
}