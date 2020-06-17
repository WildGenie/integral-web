using System.Threading.Tasks;

namespace Integral.Senders
{
    public interface EmailSender
    {
        Task Send(string recipient, string subject, string body);
    }
}
