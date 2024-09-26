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
    private readonly InvoiceRepository repository;

    public event EventHandler<Invoice> InvoiceCreated;

    public InvoiceFactory(CompanyRepository companyRepository, InvoiceRepository repository)
    {
        this.companyRepository = companyRepository;
        this.repository = repository;
    }

    public Invoice CreateInvoice(IEnumerable<InvoiceItem> items, string buyerName)
    {
        throw new NotImplementedException();

        //InvoiceCreated?.Invoke(this, invoice);
    }
}
