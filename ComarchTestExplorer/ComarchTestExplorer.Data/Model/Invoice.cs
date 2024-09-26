using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Data.Model;

public class Invoice
{
    public int Id { get; set; }

    public string Number { get; set; }

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string SuplierName { get; set; }

    public string BuyerName { get; set; }

    public IList<InvoiceItem>? Items { get; set; }
}
