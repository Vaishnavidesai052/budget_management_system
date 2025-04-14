using System.Net;
using System.Net.Mail;

namespace BudgetManagementSystemNew.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendResetPasswordEmail(string recipientEmail, string resetToken)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SmtpUser"],
                    _configuration["EmailSettings:SmtpPassword"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:SmtpUser"]),
                Subject = "Password Reset Request",
                
                Body = $"heres the link for reset  " +
                $" : http://localhost:5173/reset-password?token={resetToken}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(recipientEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
