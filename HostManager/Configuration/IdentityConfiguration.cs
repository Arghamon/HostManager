using HostManager.Contracts;
using HostManager.Data;
using HostManager.Models;
using HostManager.Repositories;
using HostManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace HostManager.Configuration
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Auth/Login";
                options.LogoutPath = $"/Auth/Logout";
                options.AccessDeniedPath = $"/Auth/AccessDenied";
            });

            services.AddScoped<IIdentitySeederService, IdentitySeederService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddScoped<IRepository<Package>, PackageRepository>();
            services.AddScoped<IRepository<Term>, TermRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddScoped<IRepository<Company>, CompanyRepository>();
            services.AddSingleton<ICheckExpirationService, CheckExpirationService>();
            services.AddSingleton<IEmailService, EmailService>();
        }
    }

    
}
