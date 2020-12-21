using HostManager.Contracts;
using HostManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostManager.Services
{
    public class CheckExpirationService : ICheckExpirationService
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private IEmailService _email;
        private readonly IWebHostEnvironment _env;
        public CheckExpirationService(IServiceScopeFactory serviceScopeFactory, IEmailService email, IWebHostEnvironment env)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _email = email;
            _env = env;
        }

        public async Task CheckExpiration()
        {

            var isMonday = DateTime.Now.DayOfWeek == DayOfWeek.Monday;
            if (!isMonday)
            {
                return;
            }
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var account = scope.ServiceProvider.GetRequiredService<IRepository<Account>>();
                var accounts = account.GetAll().ToList();
                accounts = accounts
                    .Where(a => a.PayDate.AddMonths(a.Term.Value).Subtract(DateTime.Now).TotalDays <= 8)
                    .ToList();

                if (accounts.Count >= 0)
                {
                    var body = CreateBody(accounts);
                    var mailPath = _env.WebRootPath + "/templates/mail.html";
                    var outputHtmlPath = _env.WebRootPath + "/output/invoice.html";



                    HtmlStringGenerator mail = new HtmlStringGenerator(mailPath);
                    

                    mail.AddParameters("{body}", body);
                    var mailContent = await mail.GenerateBody();
                    Console.WriteLine("Sending Email...");
                    //await _email.SendMailAsync("ioanelomidze@gmail.com", "დომენები", mailContent, null);

                    foreach (var item in accounts)
                    {

                        var outputPdfPath = await CreateInvoice(item, outputHtmlPath);

                        DocumentBuilder.CreatePdf(outputHtmlPath, outputPdfPath);

                        // await _email.SendMailAsync(item.Company.Email, "არტმედია", "დომენის საფასურის გადახდის ინვოისი", outputPdfPath);
                    }
                }
            }
        }


        private double GetPrice(int packageId, int termId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _price = scope.ServiceProvider.GetRequiredService<IPriceRepository>();
                    var price = _price.GetPriceByTerm(new PriceRequest
                    {
                        PackageId = packageId,
                        TermId = termId,
                    });

                return price;
            }
        }

        private string CheckInvoice(int companyId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var invoice = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
                var account = scope.ServiceProvider.GetRequiredService<IRepository<Account>>();
                var companyAccount = account.FindById(companyId);
                

                var existed = invoice.FindByCompany(companyId, companyAccount.PayDate.AddMonths(companyAccount.Term.Value));
                if(existed == null)
                {
                    return null;
                }

                if (!File.Exists(existed.Path))
                {
                    invoice.Delete(existed.Id);
                    return null;
                }

                return existed.Path;
                
            }
        }

        private string CreateBody(List<Account> accounts)
        {
            string body = "";
            foreach (var item in accounts)
            {
            var price = GetPrice(item.PackageId, item.TermId);
                body += $"<tr><td>{item.DomainName}</td>" +
                        $"<td>{price} ლარი</td>" +
                        $"<td>{item.PayDate.AddMonths(item.Term.Value).ToString("dd-MM-yyyy")}</td></tr>";
            }

            return body;
        }

        private async Task<string> CreateInvoice(Account item, string outputHtmlPath)
        {

            var existedInvoicePath = CheckInvoice(item.CompanyId);

            if(existedInvoicePath != null)
            {
                return existedInvoicePath;
            }

            var id = Guid.NewGuid();

            var invoice = new Invoice
            {
                CompanyId = item.CompanyId,
                ExpireDate = item.PayDate.AddMonths(item.Term.Value),
                Path = _env.WebRootPath + $"/docs/{id}-invoice.pdf",
            };

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var invoiceRepository = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
                invoiceRepository.Add(invoice);
            }


                var invoicePath = _env.WebRootPath + $"/templates/{item.Company.InvoiceTemplate}.html";
            var outputPdfPath = _env.WebRootPath + $"/docs/{id}-invoice.pdf";

            HtmlStringGenerator invoiceHtml = new HtmlStringGenerator(invoicePath);

            invoiceHtml.AddParameters("{date}", item.PayDate.AddMonths(item.Term.Value).ToString("dd-MM-yyyy"));
            invoiceHtml.AddParameters("{domain}", item.DomainName);
            invoiceHtml.AddParameters("{price}", GetPrice(item.PackageId, item.TermId).ToString());
            invoiceHtml.AddParameters("{name}", item.Company.Name);
            invoiceHtml.AddParameters("{code}", item.Company.Code);
            invoiceHtml.AddParameters("{address}", item.Company.Address);
            invoiceHtml.AddParameters("{invoiceId}", invoice.Id.ToString());
            invoiceHtml.AddParameters("{expireDate}", invoice.ExpireDate.ToString("dd/MM/yyyy"));
            invoiceHtml.AddParameters("{createDate}", DateTime.Now.ToString("dd/MM/yyyy"));

            var invoiceContent = await invoiceHtml.GenerateBody();
            await invoiceHtml.WriteAsync(outputHtmlPath, invoiceContent);

            return outputPdfPath;
        }
    }
}
