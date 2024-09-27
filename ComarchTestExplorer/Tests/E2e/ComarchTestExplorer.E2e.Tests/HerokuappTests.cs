using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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
        var option = new ChromeOptions();
        option.AddUserProfilePreference("download.default_directory", Path.Combine(Environment.CurrentDirectory, "Downloads"));
        option.AddUserProfilePreference("download.prompt_for_download", false);
        option.AddUserProfilePreference("download.directory_upgrade", true);
        option.AddUserProfilePreference("safebrowsing.enabled", true);

        //option.AddArgument("--start-maximized");
        option.AddArgument("headless=new");
        option.AddArgument("--window-size=1920,1050");
        option.AddArgument("--disable-gpu");
        option.AddArgument("--incognito");
        option.AddArgument("--disable-extensions");
        option.AddArgument("--ignore-certificate-errors");
        option.AddArgument("--disable-popup-blocking"); //!
        option.AddArgument("--lang=en-US");
        option.AddArgument("--allow-insecure-localhost");

        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver(option);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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

    [Test]
    public void HandleJavaScriptAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        Assert.That(alert.Text, Does.Contain("I am a JS Alert"));
        alert.Accept();

        var resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Does.Contain("You successfully clicked an alert"));
    }

    [Test]
    public void HandleJavaScriptAlertsWithInput()
    {
        const string inputText = "Test Selenium";
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[3]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();
        alert.SendKeys(inputText);
        alert.Accept();
        
        var resultText = driver.FindElement(By.Id("result"));
        resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Does.Contain(inputText), "Komunikat po akceptacji prompta jest nieprawidłowy!");
    }


    [Test]
    public void TestDynamicLoading()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

        var startBtn = driver.FindElement(By.XPath("//*[@id=\"start\"]/button"));
        startBtn.Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var loadedElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("finish")));
        Assert.That(loadedElement.Text, Does.Contain("Hello World!"));
    }

    [Test]
    public void TestDynamicLoadingWhenNotExists()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

        var startBtn = driver.FindElement(By.XPath("//*[@id=\"start\"]/button"));
        startBtn.Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var loadedElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("finish")));
        Assert.That(loadedElement.Text, Does.Contain("Hello World!"));
    }

    [Test]
    public void FileUploadTest()
    {
        FileInfo fileInfo = new FileInfo(Path.Combine("Assets", "UploadFileTest.txt"));
        Assert.That(fileInfo.Exists, Is.True, "Brakuje pliku testowego.");

        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/upload");
        var fileInput = driver.FindElement(By.Id("file-upload"));
        fileInput.SendKeys(fileInfo.FullName);

        var uploadBtn = driver.FindElement(By.Id("file-submit"));
        uploadBtn.Click();

        var resultInfo = driver.FindElement(By.Id("uploaded-files"));

        Assert.That(resultInfo.Text, Does.Contain(fileInfo.Name));
    }

    [Test]
    public void FileDownloadTest()
    {
        string fileName = "test.txt";
        var dir = PrepareFolder();

        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/download");
        var downloadLink = driver.FindElement(By.LinkText(fileName));
        downloadLink.Click();

        Thread.Sleep(1000);

        Assert.That(dir.GetFiles().Any(f => f.Name == fileName), Is.True);
    }

    private DirectoryInfo PrepareFolder()
    {
        string dir = "Downloads";
        DirectoryInfo directory = new DirectoryInfo(dir);

        if (!directory.Exists)
            directory.Create();

        if (directory.GetFiles().Any())
        {
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        return directory;
    }
}
