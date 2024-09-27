using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ComarchTestExplorer.E2e.Tests;

public class SeleniumTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver();
    }

    [Test]
    public void Test1()
    {
        // Arrange
        driver.Navigate().GoToUrl("https://example.com");

        // Assert
        Assert.That(driver.Title, Is.EqualTo("Example Domain"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit(); // Close the browser
    }
}