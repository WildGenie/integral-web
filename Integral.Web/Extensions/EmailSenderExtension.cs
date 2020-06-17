using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Integral.Senders;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Integral.Extensions
{
    public static class EmailSenderExtension
    {
        private const string ConfirmSubject = "Confirm your email";

        private const string ConfirmBody = "Please confirm your account by <a href='{0}'>clicking here</a>.";

        private const string ResetSubject = "Reset Password";

        private const string ResetBody = "Please reset your password by <a href='{0}'>clicking here</a>.";

        public static async Task SendEmailConfirmation(this EmailSender emailSender, string email, string link)
        {
            await emailSender.Send(email, ConfirmSubject, string.Format(ConfirmBody, HtmlEncoder.Default.Encode(link)));
        }

        public static async Task SendPasswordReset(this EmailSender emailSender, string email, string link)
        {
            await emailSender.Send(email, ResetSubject, string.Format(ResetBody, HtmlEncoder.Default.Encode(link)));
        }

        public static async Task SendErrorReport(this EmailSender emailSender, string email, HttpContext httpContext)
        {
            string body = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error?.StackTrace ?? string.Empty;
            await emailSender.Send(email, $"Error {Activity.Current?.Id ?? httpContext.TraceIdentifier}", body);
        }
    }
}
