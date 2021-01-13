using HostManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HostManager.Contracts
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogOutAsync();
    }
}
