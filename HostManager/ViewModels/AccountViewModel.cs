using HostManager.Models;
using System.Collections.Generic;

namespace HostManager.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public List<Package> Packages { get; set; }
        public List<Term> Terms { get; set; }
        public List<Company> Companies { get; set; }
        public Price Price { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}
