using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface ICheckExpirationService
    {
        public void CheckExpiration();
    }
}
