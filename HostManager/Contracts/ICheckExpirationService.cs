using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface ICheckExpirationService
    {
        public Task CheckExpiration();
    }
}
