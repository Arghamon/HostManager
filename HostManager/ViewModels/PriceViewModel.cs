using HostManager.Models;
using System.Collections.Generic;

namespace HostManager.ViewModels
{
    public class PriceViewModel
    {
        public List<Package> Packages { get; set; }
        public List<Term> Terms { get; set; }
        public Price Price { get; set; }
    }
}
