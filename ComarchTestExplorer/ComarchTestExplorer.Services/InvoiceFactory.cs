using ComarchTestExplorer.Data.Model;
using ComarchTestExplorer.Data.Repositories;
using ComarchTestExplorer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Services;

public class InvoiceFactory : IInvoiceFactory
{
    private readonly CompanyRepository companyRepository;
    private readonly InvoiceRepository invoiceRepository;

    public event EventHandler<Invoice> InvoiceCreated;

    public InvoiceFactory(CompanyRepository companyRepository, InvoiceRepository invoiceRepository)
    {
        this.companyRepository = companyRepository;
        this.invoiceRepository = invoiceRepository;
    }

    public Invoice CreateInvoice(IEnumerable<InvoiceItem> items, string buyerName)
    {
        if (items == null || !items.Any())
        {
            throw new ArgumentException("Items cannot be null or empty");
        }

        if (string.IsNullOrEmpty(buyerName))
        {
            throw new ArgumentException("Buyer name cannot be null or empty");
        }

        Invoice invoice = new()
        {
            Id = invoiceRepository.Data.Last().Id + 1,
            BuyerName = buyerName,
            IssueDate = DateTime.Now,
            Number = $"{DateTime.Now.Year}/{DateTime.Now.Month}-{invoiceRepository.Data.Count + 1}",
            Items = items.ToList(),
            TotalAmount = items.Sum(i => i.GrossValue * i.Quantity),
            SuplierName = companyRepository.GetCompany()
        };
        
        InvoiceCreated?.Invoke(this, invoice);
        return invoice;
    }
}
