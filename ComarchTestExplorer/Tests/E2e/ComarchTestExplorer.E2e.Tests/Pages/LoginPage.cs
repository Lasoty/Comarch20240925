using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.E2e.Tests.Pages;

internal class LoginPage
{
    private readonly IWebDriver driver;
    private bool isOpened;

    public LoginPage(IWebDriver webDriver)
    {
        this.driver = webDriver;
    }

    public IWebElement UserNameField => isOpened ? driver.FindElement(By.Id("username")) : throw new Exception("Strona musi być otwarta");
    public IWebElement PasswordField => driver.FindElement(By.Id("password"));
    public IWebElement LoginButton => driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));

    public void Open()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        isOpened = true;
    }

    public void LogIn(string userName, string password)
    {
        UserNameField.SendKeys(userName);
        PasswordField.SendKeys(password);
        LoginButton.Click();
    }

}
