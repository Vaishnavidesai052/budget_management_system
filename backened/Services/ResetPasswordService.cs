//using System;
//using SecureAuthApi.Models;
//using SecureAuthApi.Repositories; // Use the correct namespace for the repository
//using System.Security.Cryptography;
//using System.Text;
//using YourProject.Services;

//namespace SecureAuthApi.Services
//{
//    public class ResetPasswordService
//    {
//        private readonly UserRepository _userRepository;
//        private readonly EmailService _emailService;

//        public ResetPasswordService(UserRepository userRepository, EmailService emailService)
//        {
//            _userRepository = userRepository;
//            _emailService = emailService;
//        }

//        // Send reset password email
//        public bool SendPasswordResetEmail(string email)
//        {
//            var user = _userRepository.GetUserByEmail(email); // Ensure this is a valid method in your UserRepository
//            if (user == null) return false;

//            // Generate reset token
//            var token = Guid.NewGuid().ToString();
//            user.ResetToken = token;
//            user.ResetTokenExpiration = DateTime.Now.AddHours(1);

//            // Save token in the database
//            _userRepository.SavePasswordResetToken(user.Id, token); // Use the correct method to update the token

//            // Send email with reset link
//            _emailService.SendResetPasswordEmail(user.Email, token);

//            return true;
//        }

//        // Reset password using token
//        public bool ResetPassword(string token, string newPassword)
//        {
//            var user = _userRepository.GetUserByResetToken(token);
//            if (user == null || user.ResetTokenExpiration < DateTime.Now)
//                return false;

//            // Update user's password (hash before saving)
//            user.PasswordHash = HashPassword(newPassword); // Hash the new password
//            user.ResetToken = null;  // Clear reset token
//            user.ResetTokenExpiration = null;  // Clear expiration time

//            // Create UserUpdateModel with updated fields
//            var updateModel = new UserUpdateModel
//            {
//                Email = user.Email,
//                Username = user.Username,
//                DepartmentId = Convert.ToInt32(user.Departmentid), // Assuming Departmentid is a string, convert it to int
//                RoleId = 1, // Adjust the RoleId as needed, or fetch it from user data if necessary
//                Status = "Active" // Set the status as "Active" (or based on your logic)
//            };

//            // Now, update the user
//            _userRepository.UpdateUser(user.Id, updateModel); // Use correct method for update

//            return true;
//        }


//        // Helper method for password hashing
//        private string HashPassword(string password)
//        {
//            using var sha256 = SHA256.Create();
//            var bytes = Encoding.UTF8.GetBytes(password);
//            var hash = sha256.ComputeHash(bytes);
//            return Convert.ToBase64String(hash);
//        }
//    }
//}
using System;
using BudgetManagementSystemNew.Repositories;
using System.Security.Cryptography;
using System.Text;
using BudgetManagementSystemNew.Services;

namespace SecureAuthApi.Services
{
    public class ResetPasswordService
    {
        private readonly UserRepository _userRepository;
        private readonly EmailService _emailService;

        public ResetPasswordService(UserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        // Send reset password email
        public bool SendPasswordResetEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email); // Ensure this is a valid method in your UserRepository
            if (user == null) return false;

            // Generate reset token
            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiration = DateTime.Now.AddHours(1);

            // Save token in the database
            _userRepository.SavePasswordResetToken(user.Id, token); // Use the correct method to update the token

            // Send email with reset link
            _emailService.SendResetPasswordEmail(user.Email, token);

            return true;
        }

        // Reset password using token
        public bool ResetPassword(string token, string newPassword)
        {
            var user = _userRepository.GetUserByResetToken(token);
            if (user == null || user.ResetTokenExpiration < DateTime.Now)
                return false;

            // Update user's password (hash before saving)
            user.PasswordHash = HashPassword(newPassword); // Hash the new password
            user.ResetToken = null;  // Clear reset token
            user.ResetTokenExpiration = null;  // Clear expiration time

            // Create UserUpdateModel with updated fields
            var updateModel = new UserUpdateModel
            {
                Email = user.Email,
                Username = user.Username,
                DepartmentId = Convert.ToInt32(user.Departmentid), // Assuming Departmentid is a string, convert it to int
                RoleId = 1, // Adjust the RoleId as needed, or fetch it from user data if necessary
                Status = "Active" // Set the status as "Active" (or based on your logic)
            };

            // Now, update the user
            _userRepository.UpdateUser(user.Id, updateModel); // Use correct method for update

            return true;
        }

        // Helper method for password hashing
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
