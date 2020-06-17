using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Integral.Senders
{
    public sealed class SmtpEmailSender : EmailSender
    {
        private static readonly MailAddress mailAddress = new MailAddress("todo", "todo");

        private string username, password, host;

        private int port;

        public SmtpEmailSender(string username, string password, string host, int port)
        {
            this.username = username;
            this.password = password;
            this.host = host;
            this.port = port;
        }

        public async Task Send(string email, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.From = mailAddress;
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.EnableSsl = false;
            smtpClient.Timeout = 10000;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}