using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface IEmailService
    {
        public Task SendMailAsync(string email, string subject, string body, string path);
    }
}
