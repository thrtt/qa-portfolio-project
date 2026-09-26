using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class CartTest
    {
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string InventoryUrl = "https://www.saucedemo.com/inventory.html";
        private readonly string CartUrl = "https://www.saucedemo.com/cart.html";
        private readonly string CheckoutUrl = "https://www.saucedemo.com/checkout-step-one.html";
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
        public void AddItemToCartTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            wait.Until(d =>
                d.FindElements(By.ClassName("cart_item")).Count == 1);

            var cartItems =
                driver.FindElements(By.ClassName("cart_item"));

            var productName =
                driver.FindElement(
                    By.ClassName("inventory_item_name")).Text;

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CartUrl));
                Assert.That(cartItems.Count, Is.EqualTo(1));
                Assert.That(
                    productName,
                    Is.EqualTo("Sauce Labs Backpack"));
            });
        }

        [Test]
        public void AddMultipleItemsToCartTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-bike-light")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-bike-light")).Count > 0);

            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-bolt-t-shirt")).Count > 0);

            wait.Until(d =>
            {
                var badges = d.FindElements(By.ClassName("shopping_cart_badge"));

                return badges.Count > 0 && badges[0].Text == "3";
            });

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            wait.Until(d =>
                d.FindElements(By.ClassName("cart_item")).Count == 3);

            var cartItems =
                driver.FindElements(By.ClassName("cart_item"));

            var productNames =
                driver.FindElements(
                    By.ClassName("inventory_item_name"))
                      .Select(p => p.Text)
                      .ToList();

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CartUrl));
                Assert.That(cartItems.Count, Is.EqualTo(3));

                Assert.That(
                    productNames,
                    Does.Contain("Sauce Labs Backpack"));

                Assert.That(
                    productNames,
                    Does.Contain("Sauce Labs Bike Light"));

                Assert.That(
                    productNames,
                    Does.Contain("Sauce Labs Bolt T-Shirt"));
            });
        }

        [Test]
        public void RemoveItemFromCartTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            driver.FindElement(
                By.Id("remove-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElements(By.ClassName("cart_item")).Count == 0);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("shopping_cart_badge")).Count == 0);

            Assert.Multiple(() =>
            {
                Assert.That(
                    driver.FindElements(
                        By.ClassName("cart_item")).Count,
                    Is.EqualTo(0));

                Assert.That(
                    driver.FindElements(
                        By.ClassName("shopping_cart_badge")).Count,
                    Is.EqualTo(0));
            });
        }

        [Test]
        public void RemoveMultipleItemsFromCartTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-bike-light")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-bike-light")).Count > 0);

            wait.Until(d =>
            {
                var badge =
                    d.FindElements(By.ClassName("shopping_cart_badge"));

                return badge.Count > 0 &&
                       badge[0].Text == "2";
            });

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("cart_item")).Count == 2);

            driver.FindElement(
                By.Id("remove-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElements(
                    By.Id("remove-sauce-labs-backpack")).Count == 0);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("cart_item")).Count == 1);

            driver.FindElement(
                By.Id("remove-sauce-labs-bike-light")).Click();

            wait.Until(d =>
                d.FindElements(
                    By.Id("remove-sauce-labs-bike-light")).Count == 0);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("cart_item")).Count == 0);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("shopping_cart_badge")).Count == 0);

            Assert.Multiple(() =>
            {
                Assert.That(
                    driver.FindElements(
                        By.ClassName("cart_item")).Count,
                    Is.EqualTo(0));

                Assert.That(
                    driver.FindElements(
                        By.ClassName("shopping_cart_badge")).Count,
                    Is.EqualTo(0));
            });
        }


        [Test]
        public void ContinueShoppingTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            var continueShoppingButton = wait.Until(d =>
                d.FindElement(
                    By.XPath("//button[@id='continue-shopping']")));

            continueShoppingButton.Click();

            wait.Until(d => d.Url == InventoryUrl);

            Assert.That(
                driver.Url,
                Is.EqualTo(InventoryUrl));
        }

        [Test]
        public void CheckoutTest()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            wait.Until(d =>
                d.FindElement(By.ClassName("shopping_cart_badge")).Text == "1");

            driver.FindElement(
                By.ClassName("shopping_cart_link")).Click();

            wait.Until(d => d.Url == CartUrl);

            var checkoutButton = wait.Until(d =>
                d.FindElement(By.Id("checkout")));

            checkoutButton.Click();

            wait.Until(d => d.Url == CheckoutUrl);

            var firstName = wait.Until(d =>
                d.FindElement(By.Id("first-name")));

            var lastName =
                driver.FindElement(By.Id("last-name"));

            var postalCode =
                driver.FindElement(By.Id("postal-code"));

            Assert.Multiple(() =>
            {
                Assert.That(
                    driver.Url,
                    Is.EqualTo(CheckoutUrl));

                Assert.That(firstName.Displayed, Is.True);
                Assert.That(lastName.Displayed, Is.True);
                Assert.That(postalCode.Displayed, Is.True);
            });
        }

        [TearDown]
        public void TearDown()
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