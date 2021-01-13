using HostManager.Models;
using System;

namespace HostManager.Contracts
{
    interface IInvoiceRepository : IRepository<Invoice>
    {
        Invoice FindByCompany(int CompanyId, DateTime date);
    }
}
