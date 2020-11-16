using HostManager.Contracts;
using HostManager.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HostManager.Services
{
    public class CheckExpirationService : ICheckExpirationService
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private IEmailService _email;
        public CheckExpirationService(IServiceScopeFactory serviceScopeFactory, IEmailService email)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _email = email;
        }

        public async void CheckExpiration()
        {
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var account = scope.ServiceProvider.GetRequiredService<IRepository<Account>>();
                var accounts = account.GetAll().ToList();
                accounts = accounts.Where(a => a.RegisterDate.AddMonths(a.Term.Value)
                    .Subtract(DateTime.Now).TotalHours <= 25)
                    .ToList();

                if (accounts.Count > 0)
                {
                    var body = CreateBody(accounts);
                    Console.WriteLine("Sending Email...");
                    await _email.SendMailAsync("gogashonia@gmail.com", "დომენები", body);
                }
            }
        }

        private string CreateBody(List<Account> accounts)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _price = scope.ServiceProvider.GetRequiredService<IPriceRepository>();

                string body = "<h1>ვადა გასდის/გაუვიდა</h1><table><thead><th>დომენი</th><th>ფასი</th><th>ვადის გასვლის თარიღი</th></thead> <tbody>";

                foreach (var item in accounts)
                {
                    var price = _price.GetPriceByTerm(new PriceRequest
                    {
                        PackageId = item.PackageId,
                        TermId = item.TermId,
                    });
                    body += $"<tr><td>{item.DomainName}</td>" +
                        $"<td>{price} ლარი</td>" +
                        $"<td>{item.RegisterDate.AddMonths(item.Term.Value).ToString("dd-MM-yyyy")}</td></tr>";
                }

                body += "</tbody></table>";

                return body;
            }
        }
    }
}
