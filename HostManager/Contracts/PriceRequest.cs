using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public class PriceRequest
    {
        public int PackageId { get; set; }
        public int TermId { get; set; }
    }
}
