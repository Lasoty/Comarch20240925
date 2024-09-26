using ComarchTestExplorer.Data.Model;
using ComarchTestExplorer.Data.Repositories;
using FluentAssertions;

namespace ComarchTestExplorer.Services.Tests;

public class InvoiceFactoryTests
{
    InvoiceFactory _invoiceFactory;
    IEnumerable<InvoiceItem> _items;
    string _buyerName;

    [SetUp]
    public void Setup()
    {
        _invoiceFactory = new InvoiceFactory(new CompanyRepository(), new InvoiceRepository());

        _items = [
            new InvoiceItem
            {
                Id = 1,
                Name = "Item1",
                NetValue = 10,
                Quantity = 1,
                Tax = 0.05m,
            },
            new InvoiceItem
            {
                Id = 2,
                Name = "Item2",
                NetValue = 20,
                Quantity = 3,
                Tax = 0.1m,
            }
            ];
        _buyerName = "Buyer1";
    }

    [Test]
    public void CreateInvoice_WhenItemsAreNull_ThrowsArgumentException()
    {
        // Arrange
        IEnumerable<InvoiceItem> items = null;
        string buyerName = "Buyer1";

        // Act
        Action act = () => _invoiceFactory.CreateInvoice(items, buyerName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Items cannot be null or empty");
    }

    [Test]
    public void CreateInvoice_WhenItemsAreEmpty_ThrowsArgumentException()
    {
        // Arrange
        IEnumerable<InvoiceItem> items = new List<InvoiceItem>();
        string buyerName = "Buyer1";

        // Act
        Action act = () => _invoiceFactory.CreateInvoice(items, buyerName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Items cannot be null or empty");
    }

    [Test]
    public void CreateInvoice_WhenBuyerNameIsNull_ThrowsArgumentException()
    {
        // Arrange
        IEnumerable<InvoiceItem> items = new List<InvoiceItem> { new InvoiceItem() };
        string buyerName = null;

        // Act
        Action act = () => _invoiceFactory.CreateInvoice(items, buyerName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Buyer name cannot be null or empty");
    }

    [Test]
    public void CreateInvoice_WhenBuyerNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        IEnumerable<InvoiceItem> items = new List<InvoiceItem> { new InvoiceItem() };
        string buyerName = string.Empty;

        // Act
        Action act = () => _invoiceFactory.CreateInvoice(items, buyerName);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Buyer name cannot be null or empty");
    }

    [Test]
    public void CreateInvoice_WhenItemsAndBuyerNameAreValid_ReturnsInvoice()
    {
        // Act
        var result = _invoiceFactory.CreateInvoice(_items, _buyerName);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(2);
        result.BuyerName.Should().Be(_buyerName);
        result.IssueDate.Should().BeOnOrAfter(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
        result.Number.Should().Be($"{DateTime.Now.Year}/{DateTime.Now.Month}-2");
        result.Items.Should().BeEquivalentTo(_items);
        result.TotalAmount.Should().Be(76.5m);
        result.SuplierName.Should().NotBeNullOrEmpty().And.StartWith("Comarch").And.EndWith("Szkolenia");
    }

    [Test]
    public void GenerateInvoiceItems_AllItems_ShouldHavePositiveQuantity()
    {
        // Act
        var result = _invoiceFactory.CreateInvoice(_items, _buyerName);

        // Assert
        result.Items.Should().OnlyContain(item => item.Quantity > 0);
    }

    [Test]
    //Sprawdzić czy InvoiceFactory wywołuje zdarzenie InvoiceCreated za pomocą Monitora
    public void CreateInvoice_WhenItemsAndBuyerNameAreValid_RaisesInvoiceCreatedEvent()
    {
        // Arrange
        using var monitoredSubject = _invoiceFactory.Monitor();
        _invoiceFactory.InvoiceCreated += (_, _) => { };

        // Act
        _invoiceFactory.CreateInvoice(_items, _buyerName);

        // Assert
        monitoredSubject.Should().Raise(nameof(InvoiceFactory.InvoiceCreated));
    }

}
