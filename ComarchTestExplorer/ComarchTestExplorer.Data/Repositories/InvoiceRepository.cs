﻿using ComarchTestExplorer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Data.Repositories;

public class InvoiceRepository
{
    public IList<Invoice> Data { get; set; }

    public InvoiceRepository()
    {
        Data = [
            new Invoice{
                BuyerName = "Buyer1",
                Id = 1,
                IssueDate = new DateTime(2024,09,02),
                Number = "2024/09-1",
                Items = [
                    new() {
                        GrossValue = 100,
                        Id = 1,
                        Name = "Item1",
                        NetValue = 80,
                        Quantity = 1,
                        Tax = 20
                    },
                    new InvoiceItem{
                        GrossValue = 200,
                        Id = 2,
                        Name = "Item2",
                        NetValue = 160,
                        Quantity = 1,
                        Tax = 40
                    }
                ],
            }
            ];
    }
}
