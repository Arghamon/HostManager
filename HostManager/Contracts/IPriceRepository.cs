using HostManager.Models;

namespace HostManager.Contracts
{
    public interface IPriceRepository : IRepository<Price>
    {
        double GetPriceByTerm(PriceRequest request);
    }
}
