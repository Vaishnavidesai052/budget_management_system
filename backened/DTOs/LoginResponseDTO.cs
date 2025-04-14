using SecureAuthApi.Models;

namespace BudgetManagementSystemNew.DTOs
{
    public class LoginResponseDTO
    {
        
            public string Token { get; set; } // JWT token for authentication
            public string Username { get; set; } // Username of the logged-in user
            public User UserDetails { get; set; } // User details (Id, Email, Role, etc.)
        
    }
}
