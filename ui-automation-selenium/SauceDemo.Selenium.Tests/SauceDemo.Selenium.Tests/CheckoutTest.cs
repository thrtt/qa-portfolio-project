using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class CheckoutTest
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string InventoryUrl = "https://www.saucedemo.com/inventory.html";
        private readonly string CartUrl = "https://www.saucedemo.com/cart.html";

        private readonly string CheckoutUrl = "https://www.saucedemo.com/checkout-step-one.html";
        private readonly string OverviewUrl = "https://www.saucedemo.com/checkout-step-two.html";
        private readonly string CompleteUrl = "https://www.saucedemo.com/checkout-complete.html";

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

        // CHTC-001 - CHS-001
        [Test]
        public void CompleteCheckoutWithValidDataTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            wait.Until(d => d.FindElement(By.Id("checkout"))).Click();
            wait.Until(d => d.Url == CheckoutUrl);
            wait.Until(d => d.FindElement(By.Id("first-name")).Displayed);

            driver.FindElement(By.Id("first-name")).SendKeys("Ivan");
            driver.FindElement(By.Id("last-name")).SendKeys("Petrov");
            driver.FindElement(By.Id("postal-code")).SendKeys("1000");
            driver.FindElement(By.Id("continue")).Click();

            wait.Until(d => d.Url == OverviewUrl);
            wait.Until(d => d.FindElement(By.Id("finish"))).Click();
            wait.Until(d => d.Url == CompleteUrl);

            var message = wait.Until(d =>
                d.FindElement(By.ClassName("complete-header")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CompleteUrl));
                Assert.That(message.Displayed, Is.True);
                Assert.That(message.Text, Is.EqualTo("Thank you for your order!"));
            });
        }

        // CHTC-002 - CHS-002
        [Test]
        public void CheckoutWithEmptyFirstNameTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            wait.Until(d => d.FindElement(By.Id("checkout"))).Click();
            wait.Until(d => d.Url == CheckoutUrl);
            wait.Until(d => d.FindElement(By.Id("first-name")).Displayed);

            driver.FindElement(By.Id("last-name")).SendKeys("Petrov");
            driver.FindElement(By.Id("postal-code")).SendKeys("1000");
            driver.FindElement(By.Id("continue")).Click();

            var error = wait.Until(d =>
                d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CheckoutUrl));
                Assert.That(error.Displayed, Is.True);
                Assert.That(error.Text, Is.EqualTo("Error: First Name is required"));
            });
        }

        // CHTC-003 - CHS-003
        [Test]
        public void CheckoutWithEmptyLastNameTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            wait.Until(d => d.FindElement(By.Id("checkout"))).Click();
            wait.Until(d => d.Url == CheckoutUrl);
            wait.Until(d => d.FindElement(By.Id("first-name")).Displayed);

            driver.FindElement(By.Id("first-name")).SendKeys("Ivan");
            driver.FindElement(By.Id("postal-code")).SendKeys("1000");
            driver.FindElement(By.Id("continue")).Click();

            var error = wait.Until(d =>
                d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CheckoutUrl));
                Assert.That(error.Displayed, Is.True);
                Assert.That(error.Text, Is.EqualTo("Error: Last Name is required"));
            });
        }

        // CHTC-004 - CHS-004
        [Test]
        public void CheckoutWithEmptyPostalCodeTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            wait.Until(d => d.FindElement(By.Id("checkout"))).Click();
            wait.Until(d => d.Url == CheckoutUrl);
            wait.Until(d => d.FindElement(By.Id("first-name")).Displayed);

            driver.FindElement(By.Id("first-name")).SendKeys("Ivan");
            driver.FindElement(By.Id("last-name")).SendKeys("Petrov");
            driver.FindElement(By.Id("continue")).Click();

            var error = wait.Until(d =>
                d.FindElement(By.CssSelector("[data-test='error']")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CheckoutUrl));
                Assert.That(error.Displayed, Is.True);
                Assert.That(error.Text, Is.EqualTo("Error: Postal Code is required"));
            });
        }

        // CHTC-005 - CHS-005
        [Test]
        public void CancelCheckoutTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            wait.Until(d => d.Url == CartUrl);

            wait.Until(d => d.FindElement(By.Id("checkout"))).Click();
            wait.Until(d => d.Url == CheckoutUrl);
            wait.Until(d => d.FindElement(By.Id("first-name")).Displayed);

            driver.FindElement(By.Id("first-name")).SendKeys("Ivan");
            driver.FindElement(By.Id("last-name")).SendKeys("Petrov");
            driver.FindElement(By.Id("postal-code")).SendKeys("1000");
            driver.FindElement(By.Id("continue")).Click();

            wait.Until(d => d.Url == OverviewUrl);
            wait.Until(d => d.FindElement(By.Id("cancel"))).Click();
            wait.Until(d => d.Url == InventoryUrl);

            var inventory = wait.Until(d =>
                d.FindElement(By.Id("inventory_container")));

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(InventoryUrl));
                Assert.That(inventory.Displayed, Is.True);
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
