using ComarchTestExplorer.Data.Model;

namespace ComarchTestExplorer.Data.Repositories;

public class InvoiceRepository
{
    public IList<Invoice> Data { get; set; }

    public InvoiceRepository()
    {
        Data = new List<Invoice> {
            new Invoice{
                BuyerName = "Buyer1",
                Id = 1,
                IssueDate = new DateTime(2024,09,02),
                Number = "2024/09-1",
                Items = new List<InvoiceItem>{
                    new() {
                        Id = 1,
                        Name = "Item1",
                        NetValue = 80,
                        Quantity = 1,
                        Tax = 20
                    },
                    new InvoiceItem{
                        Id = 2,
                        Name = "Item2",
                        NetValue = 160,
                        Quantity = 1,
                        Tax = 40
                    }
                },
            }
            };
    }


    public IEnumerable<Invoice> GetAll()
    {
        return Data;
    }

}
