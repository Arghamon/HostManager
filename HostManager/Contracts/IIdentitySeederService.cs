using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface IIdentitySeederService
    {
        Task Seed();
    }
}
