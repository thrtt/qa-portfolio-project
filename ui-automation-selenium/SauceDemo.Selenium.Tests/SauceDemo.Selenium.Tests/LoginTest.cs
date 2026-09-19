using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemo.Selenium.Tests
{
    public class LoginTest
    {
        private IWebDriver driver;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string InventoryUrl = "https://www.saucedemo.com/inventory.html";
        private readonly string CartUrl = "https://www.saucedemo.com/cart.html";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [Test,Order(1)]
        public void ValidLogin() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            
            Assert.That(driver.Url, Is.EqualTo(InventoryUrl));
        }
        [Test, Order(2)]
        public void LoginWithInvalidUsername() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("wrong_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            var error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username and password do not match any user in this service"));
        }

        [Test, Order(3)]
        public void LoginWithInvalidPassword() 
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("wrong_password");
            driver.FindElement(By.Id("login-button")).Click();

            var error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username and password do not match any user in this service"));
        }

        [Test,Order(4)]
        public void LoginWithEmptyUsername()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            var error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username is required"));
        }
        
        [Test, Order(5)]
        public void LoginWithEmptyPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("");
            driver.FindElement(By.Id("login-button")).Click();

            var error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Password is required"));
        }

        [Test, Order(6)]
        public void LoginWithEmptyUsernameAndPassword()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("");
            driver.FindElement(By.Id("login-button")).Click();

            var error = driver.FindElement(By.CssSelector("[data-test='error']"));

            Assert.That(error.Text, Is.EqualTo(
                "Epic sadface: Username is required"));
        }

        [Test, Order(7)]
        public void LoginWithLockedOutUser()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("locked_out_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            var error = driver.FindElement(By.CssSelector("[data-test='error']"));
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
            driver.Quit();
            driver.Dispose();
        }
    }

}
