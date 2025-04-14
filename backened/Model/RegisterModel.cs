//namespace SecureAuthApi.Models
//{
//    public class RegisterModel
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }
//}
namespace SecureAuthApi.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int DepartmentId { get; set; }  // Add DepartmentId
        public int RoleId { get; set; }  // Add RoleId
        public string ConfirmPassword { get; set; }  // For password confirmation
    }
}
