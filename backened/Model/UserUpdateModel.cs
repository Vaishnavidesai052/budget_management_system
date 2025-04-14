public class UserUpdateModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public int RoleId { get; set; }
    public string Status { get; set; }  // Status should match the possible values ('Active' or 'Inactive')
}
