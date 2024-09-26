using ComarchTestExplorer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Services.Interfaces;

public interface IInvoiceFactory
{
    Invoice CreateInvoice(IEnumerable<InvoiceItem> items, string buyerName);
}
