using System.Collections.Generic;
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
