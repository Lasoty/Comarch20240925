using ComarchTestExplorer.Services.Interfaces;

namespace ComarchTestExplorer.Services.Tests
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        [TestCase(10, 0.23, 12.3)]
        [TestCase(20, 0.23, 24.6)]
        public void GetGrossFromNetShouldReturnValidGrossValue(decimal netValue, decimal tax, decimal expected)
        {
            // Arrange
            ICalculatorService sut = new CalculatorService();

            // Act
            decimal result = sut.GetGrossFromNet(netValue, tax);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetGrossFromNetShouldThrowExceptionWhenTaxIsNegative()
        {
            // Arrange
            ICalculatorService sut = new CalculatorService();

            // Act
            TestDelegate act = () => sut.GetGrossFromNet(10, -0.23m);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.That(act, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
