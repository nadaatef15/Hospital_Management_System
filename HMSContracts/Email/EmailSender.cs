using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net.Mail;
using System.Net;
namespace HMSContracts.Email
{
    public interface IEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
    public class EmailSender : IEmail
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;


            var bodyBuilder = new BodyBuilder { HtmlBody = message };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]!), MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);


            //var smtpClient = new System.Net.Mail.SmtpClient(emailSettings["SmtpServer"])
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]),
            //    EnableSsl = true,
            //};

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress(emailSettings["SenderEmail"]!),
            //    Subject = subject,
            //    Body = message,
            //    IsBodyHtml = true,
            //};
            //mailMessage.To.Add(toEmail);

            //await smtpClient.SendMailAsync(mailMessage);


        }
    }
}
