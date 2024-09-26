using ComarchTestExplorer.Data.Model;
using ComarchTestExplorer.Data.Repositories;
using ComarchTestExplorer.Services.Interfaces;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Services.Tests;

[TestFixture]
public class MoqInvoiceFactoryTests
{
    InvoiceFactory _invoiceFactory;
    Mock<IInvoiceRepository> _invoiceRepository;
    Mock<IDiscountService> _discountService;

    [SetUp]
    public void Setup()
    {
        _discountService = new();
        Mock<ICompanyRepository> companyRepository = new();
        _invoiceRepository = new();

        var invoices = GenerateInvoices();
        _invoiceRepository.Setup(x => x.Data).Returns(invoices);

        _invoiceFactory = new InvoiceFactory(
            companyRepository.Object,
            _invoiceRepository.Object,
            _discountService.Object
            );
    }

    private IList<Invoice> GenerateInvoices()
    {
        return new List<Invoice> {
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

    private IList<InvoiceItem> GetInvoiceItems()
    {
        return new List<InvoiceItem>
        {
            new() {
                Id = 1,
                Name = "Item1",
                NetValue = 80,
                Quantity = 1,
                Tax = 0.2m
            },
            new() {
                Id = 2,
                Name = "Item2",
                NetValue = 160,
                Quantity = 1,
                Tax = 0.4m
            }
        };
    }

    [Test]
    public void CreateInvoice_WhenAllIsGood_ShouldReturnInvoice()
    {
        // Arrange
        var items = GetInvoiceItems();

        // Act
        var invoice = _invoiceFactory.CreateInvoice(items, "Buyer1");

        // Assert
        invoice.TotalAmount.Should().BeGreaterThan(200);
    }

    [Test]
    public void CreateInvoice_WhenCalled_ThenCalculateDiscountWasCalled()
    {
        // Arrange
        _discountService.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Verifiable();
        var items = GetInvoiceItems();

        // Act
        var invoice = _invoiceFactory.CreateInvoice(items, "Buyer1");

        // Assert
        _discountService.Verify(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()), Times.Once());
    }

    [Test]
    public void CreateInvoice_WhenCalled_ThenUseCalculateDiscountValueInTotalAmount()
    {
        // Arrange
        _discountService.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10);
        var items = GetInvoiceItems();

        // Act
        var invoice = _invoiceFactory.CreateInvoice(items, "Buyer1");

        // Assert
        invoice.TotalAmount.Should().Be(310);
    }

    [Test]
    public void CreateInvoice_WhenCustomerEqualsCompany_ThenUseCalculateDiscountValueInTotalAmount()
    {
        // Arrange
        //_discountService.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.Is<string>(x => x.Equals("Customer"))))
        //    .Returns(20);

        decimal returns = 0;
        _discountService.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
            .Callback<decimal, string>((_, customerType) =>
                {
                    returns = customerType == "Customer" ? 20 : 10;
                })
            .Returns(() => returns);

        var items = GetInvoiceItems();

        // Act
        var invoice = _invoiceFactory.CreateInvoice(items, "Company");

        // Assert
        invoice.TotalAmount.Should().Be(300);
    }
}
