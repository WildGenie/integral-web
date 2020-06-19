using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Integral.Options;

namespace Integral.Senders
{
    public sealed class SmtpEmailSender : EmailSender
    {
        private readonly MailAddress mailAddress;

        private readonly SmtpClient smtpClient = new SmtpClient();

        public SmtpEmailSender(EmailSenderOptions emailSenderOptions)
        {
            this.mailAddress = new MailAddress(emailSenderOptions.Address, emailSenderOptions.Display);
            this.smtpClient.Credentials = new NetworkCredential(emailSenderOptions.Username, emailSenderOptions.Password);
            this.smtpClient.EnableSsl = emailSenderOptions.EnableSsl;
            this.smtpClient.Timeout = emailSenderOptions.Timeout;
            this.smtpClient.Host = emailSenderOptions.Host;
            this.smtpClient.Port = emailSenderOptions.Port;
        }

        public async Task Send(string email, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.From = mailAddress;
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            await this.smtpClient.SendMailAsync(mailMessage);
        }
    }
}