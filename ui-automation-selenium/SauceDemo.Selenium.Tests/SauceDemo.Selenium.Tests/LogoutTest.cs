using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class LogoutTest
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string InventoryUrl = "https://www.saucedemo.com/inventory.html";
        private readonly string CartUrl = "https://www.saucedemo.com/cart.html";

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

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            wait.Until(d => d.Url == InventoryUrl);

            wait.Until(d =>
                d.FindElements(By.Id("add-to-cart-sauce-labs-backpack")).Count > 0);
        }

        [Test]
        public void LogoutFromInventoryTest()
        {
            driver.FindElement(By.Id("react-burger-menu-btn")).Click();

            var logoutButton = wait.Until(d =>
            {
                var button = d.FindElement(By.Id("logout_sidebar_link"));
                return button.Displayed && button.Enabled ? button : null;
            });

            logoutButton.Click();

            wait.Until(d => d.Url == BaseUrl);

            var username = wait.Until(d => d.FindElement(By.Id("user-name")));
            var password = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.Id("login-button"));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(BaseUrl));
                Assert.That(username.Displayed, Is.True);
                Assert.That(password.Displayed, Is.True);
                Assert.That(loginButton.Displayed, Is.True);
                Assert.That(username.GetAttribute("value"), Is.Empty);
                Assert.That(password.GetAttribute("value"), Is.Empty);
            });
        }

        [Test]
        public void LogoutFromCartTest()
        {
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            driver.FindElement(By.Id("react-burger-menu-btn")).Click();

            var logoutButton = wait.Until(d =>
            {
                var button = d.FindElement(By.Id("logout_sidebar_link"));
                return button.Displayed && button.Enabled ? button : null;
            });

            logoutButton.Click();

            wait.Until(d => d.Url == BaseUrl);

            var loginButton = wait.Until(d => d.FindElement(By.Id("login-button")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(BaseUrl));
                Assert.That(loginButton.Displayed, Is.True);
            });
        }

        [Test]
        public void CannotOpenInventoryAfterLogoutTest()
        {
            driver.FindElement(By.Id("react-burger-menu-btn")).Click();

            var logoutButton = wait.Until(d =>
            {
                var button = d.FindElement(By.Id("logout_sidebar_link"));
                return button.Displayed && button.Enabled ? button : null;
            });

            logoutButton.Click();

            wait.Until(d => d.Url == BaseUrl);

            driver.Navigate().GoToUrl(InventoryUrl);

            wait.Until(d => d.Url == BaseUrl);

            var error = wait.Until(d =>
                d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(BaseUrl));
                Assert.That(error.Text, Is.EqualTo(
                    "Epic sadface: You can only access '/inventory.html' when you are logged in."));
                Assert.That(
                    driver.FindElements(By.ClassName("inventory_item")).Count,
                    Is.EqualTo(0));
            });
        }

        [Test]
        public void LoginAgainAfterLogoutTest()
        {
            driver.FindElement(By.Id("react-burger-menu-btn")).Click();

            var logoutButton = wait.Until(d =>
            {
                var button = d.FindElement(By.Id("logout_sidebar_link"));
                return button.Displayed && button.Enabled ? button : null;
            });

            logoutButton.Click();

            wait.Until(d => d.Url == BaseUrl);

            wait.Until(d => d.FindElement(By.Id("user-name")))
                .SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            wait.Until(d => d.Url == InventoryUrl);
            wait.Until(d =>
                d.FindElements(By.ClassName("inventory_item")).Count > 0);

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(InventoryUrl));
                Assert.That(
                    driver.FindElements(By.ClassName("inventory_item")).Count,
                    Is.EqualTo(6));
            });
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
