using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class LoginTest
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string InventoryUrl = "https://www.saucedemo.com/inventory.html";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
                options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1440,900");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [Test,Order(1)]
        public void ValidLogin() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            
            wait.Until(d => d.Url == InventoryUrl);
            Assert.That(driver.Url, Is.EqualTo(InventoryUrl));
        }
        [Test, Order(2)]
        public void LoginWithInvalidUsername() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("wrong_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username and password do not match any user in this service"));
        }

        [Test, Order(3)]
        public void LoginWithInvalidPassword() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("wrong_password");
            driver.FindElement(By.Id("login-button")).Click();

            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username and password do not match any user in this service"));
        }

        [Test,Order(4)]
        public void LoginWithEmptyUsername()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username is required"));
        }
        
        [Test, Order(5)]
        public void LoginWithEmptyPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("");
            driver.FindElement(By.Id("login-button")).Click();

            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Password is required"));
        }

        [Test, Order(6)]
        public void LoginWithEmptyUsernameAndPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("");
            driver.FindElement(By.Id("login-button")).Click();

            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username is required"));
        }

        [Test, Order(7)]
        public void LoginWithLockedOutUser()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("locked_out_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            var error = wait.Until(d => d.FindElement(By.CssSelector("[data-test='error']")));
            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Sorry, this user has been locked out."));
        }

        [Test, Order(8)]
        public void PasswordFieldMasksEnteredCharacters()
        {
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("secret_sauce");
            Assert.That(passwordField.GetAttribute("type"), Is.EqualTo("password"));
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                if (driver is ITakesScreenshot screenshotDriver &&
                    TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    var screenshotPath = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory,
                        TestContext.CurrentContext.Test.Name + ".png");
                    screenshotDriver.GetScreenshot().SaveAsFile(screenshotPath);
                    TestContext.AddTestAttachment(screenshotPath);
                }
            }
            catch (WebDriverException ex)
            {
                TestContext.WriteLine("Could not capture screenshot: " + ex.Message);
            }
            finally
            {
                driver?.Dispose();
            }
        }
    }

}
