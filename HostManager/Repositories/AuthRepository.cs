using HostManager.Contracts;
using HostManager.Models;
using HostManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HostManager.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                UserName = model.Email.Substring(0, model.Email.IndexOf("@")),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            var result = await _signInManager
                .PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
