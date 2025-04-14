namespace SecureAuthApi.Models
{
    public class Role
    {
        public int RoleID { get; set; }  // Primary key, auto-incremented
        public string RoleName { get; set; }  // The name of the role
        public string Status { get; set; }  // The status of the role (Active or Inactive)
        public DateTime CreatedAt { get; set; }  // Timestamp for when the role was created
        public DateTime UpdatedAt { get; set; }  // Timestamp for when the role was last updated
    }
}
