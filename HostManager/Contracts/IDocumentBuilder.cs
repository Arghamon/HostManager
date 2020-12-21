using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface IDocumentBuilder
    {
        Task<string> GenerateMailBody(string body);

        Task<string> GenerateInvoiceBody();

        Task<string> CreatePdf(Dictionary<string, string> invoiceParams);

    }
}
