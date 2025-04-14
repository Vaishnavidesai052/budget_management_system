//namespace SecureAuthApi.Models
//{
//    public class User
//    {
//        public int Id { get; set; } // Primary key
//        public string Email { get; set; }
//        public string PasswordHash { get; set; }
//        public string Username { get; set; }
//        public string Departmentid { get; set; }
//    }
//}
namespace SecureAuthApi.Models
{
    public class User
    {
        public int Id { get; set; } // Primary key
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public string Departmentid { get; set; }

        public int? RoleId { get; set; }  // ✅ Store only RoleID
        public string Role_Name {  get; set; }

        // Add these fields for password reset functionality
        public string ResetToken { get; set; }  // Token for password reset
        public DateTime? ResetTokenExpiration { get; set; }  // Expiration time for the reset token
    }
}
