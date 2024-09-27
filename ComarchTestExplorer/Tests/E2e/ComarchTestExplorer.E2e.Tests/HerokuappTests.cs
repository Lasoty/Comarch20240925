using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.E2e.Tests;

public class HerokuappTests
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

    [Test]
    public void CorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        IWebElement usernameField = driver.FindElement(By.Id("username"));
        IWebElement passwordField = driver.FindElement(By.Id("password"));

        usernameField.SendKeys("tomsmith");
        passwordField.SendKeys("SuperSecretPassword!");

        IWebElement loginBtn = driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
        loginBtn.Click();

        IWebElement successMessage = driver.FindElement(By.Id("flash"));

        Assert.That(successMessage.Text, Does.Contain("You logged into a secure area!"));
    }
}
