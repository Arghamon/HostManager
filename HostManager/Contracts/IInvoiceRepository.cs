using HostManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Contracts
{
    interface IInvoiceRepository : IRepository<Invoice>
    {
        Invoice FindByCompany(int CompanyId, DateTime date);
    }
}
