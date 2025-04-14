using Microsoft.AspNetCore.Mvc;
using BudgetManagementSystemNew.Model;
using BudgetManagementSystemNew.Services;
using SecureAuthApi.Services;


namespace BudgetManagementSystemNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly ResetPasswordService _resetPasswordService;

        public ResetPasswordController(ResetPasswordService resetPasswordService)
        {
            _resetPasswordService = resetPasswordService;
        }

        // Request reset email
        [HttpPost("request-reset")]
        public IActionResult RequestReset([FromBody] ForgotPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            var result = _resetPasswordService.SendPasswordResetEmail(model.Email);
            if (result)
                return Ok(new { message = "Password reset email sent." });

            return NotFound(new { message = "User not found." });
        }

        // Reset the password using token
        //[HttpPost("reset-password")]
        //public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        //{
        //    if (model.NewPassword != model.ConfirmPassword)
        //    {
        //        return BadRequest(new { message = "Passwords do not match." });
        //    }

        //    var result = _resetPasswordService.ResetPassword(model.Token, model.NewPassword);
        //    if (result)
        //        return Ok(new { message = "Password has been successfully reset." });

        //    return BadRequest(new { message = "Invalid token or token expired." });
        //}
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            Console.WriteLine($"Token received: {model.Token}"); // Log token

            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { message = "Passwords do not match." });
            }

            var result = _resetPasswordService.ResetPassword(model.Token, model.NewPassword);
            if (result)
                return Ok(new { message = "Password has been successfully reset." });

            return BadRequest(new { message = "Invalid token or token expired." });
        }

    }
}
