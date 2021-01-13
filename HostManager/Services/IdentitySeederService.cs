using HostManager.Contracts;
using HostManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class IdentitySeederService : IIdentitySeederService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentitySeederService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Seed()
        {
            var existed = await _userManager.FindByEmailAsync("welcome@artmedia.ge");

            if (existed != null)
                return;

            var user = new ApplicationUser
            {
                Email = "welcome@artmedia.ge",
                Firstname = "გოგა",
                Lastname = "შონია",
                UserName = "artmedia",
            };

            await _userManager.CreateAsync(user, "artpass123");
        }
    }
}
