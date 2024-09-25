using ComarchTestExplorer.Services.Interfaces;

namespace ComarchTestExplorer.Services.Tests;

[TestFixture]
public class DateHelperTests
{
    IDateHelper sut;

    [SetUp]
    public void Setup()
    {
        sut = new DateHelper();
    }

    [Test]
    public void AreDatesWithinRange_AllDatesWithinRange_ReturnsTrue()
    {
        // Arrange
        DateTime dateFrom = new(2020, 1, 1);
        DateTime dateTo = new(2020, 12, 31);
        List<DateTime> dates =
            [
                new(2020, 3, 15),
                new(2020, 6, 10),
                new(2020, 12, 1)
            ];

        // Act
        bool result = sut.AreDatesWithinRange(dateFrom, dateTo, dates);

        // Assert
        Assert.That(result, Is.True, "Wszystkie daty powinny mieścić się w zakresie");
    }

    [Test]
    public void AreDatesWithinRange_AtleastOneDateOutsideRange_ReturnsFalse()
    {
        // Arrange
        DateTime dateFrom = new(2020, 1, 1);
        DateTime dateTo = new(2020, 12, 31);

        List<DateTime> dates =
            [
                new DateTime(2020, 3, 15),
                new DateTime(2020, 6, 10),
                new DateTime(2021, 1, 1) // Ta data jest poza zakresem
            ];

        // Act
        bool result = sut.AreDatesWithinRange(dateFrom, dateTo, dates);

        // Assert
        Assert.That(result, Is.False, "Przynajmniej jedna data jest poza zakresem, wynik powinien być false");
    }
}
