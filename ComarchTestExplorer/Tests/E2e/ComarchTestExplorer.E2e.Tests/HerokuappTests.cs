﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

    [Test]
    public void IncorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var usernameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));

        usernameField.SendKeys("wrongUser");
        passwordField.SendKeys("wrongPassword");

        var loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        loginButton.Click();

        var errorMessage = driver.FindElement(By.Id("flash"));
        Assert.That(errorMessage.Text, Does.Contain("Your username is invalid!"), "Niepoprawne dane logowania nie wywołały błędu.");
    }

    [Test]
    public void CheckboxesTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");
        var checkbox = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[2]"));
        bool isChecked = checkbox.Selected;
        Assert.That(isChecked, Is.True, "Checkbox is unchecked");
    }

    //[Test]
    //public void RadioButtonsTest()
    //{
    //    driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/radio_buttons");
    //    var radioButton = driver.FindElement(By.XPath("//*[@id=\"radio-buttons\"]/input[2]"));
    //    radioButton.Click();
    //    bool isChecked = radioButton.Selected;
    //    Assert.That(isChecked, Is.True, "Radio button is not selected");
    //}   

    [Test]
    public void DropdownTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");
        var dropdown = driver.FindElement(By.Id("dropdown"));
        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByValue("2");
        Assert.That(selectElement.SelectedOption.Text, Is.EqualTo("Option 2"), "Incorrect dropdown value selected");
    }



}
