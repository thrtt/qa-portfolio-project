using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Globalization;

namespace SauceDemo.Selenium.Tests
{
    public class InventoryTest
    {
        private IWebDriver driver = null!;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
        private readonly string CartUrl = "https://www.saucedemo.com/cart.html";
        private WebDriverWait wait = null!;

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
            driver.Navigate().GoToUrl(BaseUrl);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            wait.Until(d => d.Url == BaseUrl + "inventory.html");
            wait.Until(d => d.FindElements(By.ClassName("inventory_item")).Count > 0);
        }

        [Test]
        public void AllProductsAreDisplayed()
        {
            var products = driver.FindElements(By.ClassName("inventory_item"));

            Assert.That(products.Count, Is.EqualTo(6));
        }

        [Test]
        public void ProductsAreDisplayedWithCorrectInformation()
        {
            var products = driver.FindElements(By.ClassName("inventory_item"));

            Assert.That(products.Count, Is.EqualTo(6));

            foreach (var product in products)
            {
                var name =
                    product.FindElement(By.ClassName("inventory_item_name"));

                var description =
                    product.FindElement(By.ClassName("inventory_item_desc"));

                var price =
                    product.FindElement(By.ClassName("inventory_item_price"));

                var image =
                    product.FindElement(By.CssSelector("img.inventory_item_img"));

                var addToCartButton =
                    product.FindElement(By.ClassName("btn_inventory"));

                Assert.Multiple(() =>
                {
                    Assert.That(name.Displayed, Is.True);
                    Assert.That(name.Text, Is.Not.Empty);

                    Assert.That(description.Displayed, Is.True);
                    Assert.That(description.Text, Is.Not.Empty);

                    Assert.That(price.Displayed, Is.True);
                    Assert.That(price.Text, Is.Not.Empty);

                    Assert.That(image.Displayed, Is.True);
                    Assert.That(addToCartButton.Displayed, Is.True);
                });
            }
        }

        [Test]
        public void AddProductToCart()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            Assert.That(
                driver.FindElement(By.ClassName("shopping_cart_badge")).Text,
                Is.EqualTo("1"));

            driver.Navigate().GoToUrl(CartUrl);

            var productNames =
                driver.FindElements(By.ClassName("inventory_item_name"))
                      .Select(p => p.Text)
                      .ToList();

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CartUrl));
                Assert.That(productNames.Count, Is.EqualTo(1));
                Assert.That(productNames,
                    Does.Contain("Sauce Labs Backpack"));
            });
        }

        [Test]
        public void AddMultipleProductsToCart()
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

            Assert.That(
                driver.FindElement(By.ClassName("shopping_cart_badge")).Text,
                Is.EqualTo("3"));

            driver.Navigate().GoToUrl(CartUrl);

            var productNames =
                driver.FindElements(By.ClassName("inventory_item_name"))
                      .Select(p => p.Text)
                      .ToList();

            Assert.Multiple(() =>
            {
                Assert.That(driver.Url, Is.EqualTo(CartUrl));
                Assert.That(productNames.Count, Is.EqualTo(3));

                Assert.That(productNames,
                    Does.Contain("Sauce Labs Backpack"));

                Assert.That(productNames,
                    Does.Contain("Sauce Labs Bike Light"));

                Assert.That(productNames,
                    Does.Contain("Sauce Labs Bolt T-Shirt"));
            });
        }

        [Test]
        public void RemoveProductFromInventory()
        {
            driver.FindElement(
                By.Id("add-to-cart-sauce-labs-backpack")).Click();

            wait.Until(d => d.FindElements(By.Id("remove-sauce-labs-backpack")).Count > 0);

            Assert.That(
                driver.FindElement(By.ClassName("shopping_cart_badge")).Text,
                Is.EqualTo("1"));

            var removeButton =
                driver.FindElement(
                    By.Id("remove-sauce-labs-backpack"));

            Assert.That(removeButton.Text, Is.EqualTo("Remove"));

            removeButton.Click();

            var addToCartButton =
                driver.FindElement(
                    By.Id("add-to-cart-sauce-labs-backpack"));

            var cartBadge =
                driver.FindElements(
                    By.ClassName("shopping_cart_badge"));

            Assert.Multiple(() =>
            {
                Assert.That(
                    addToCartButton.Text,
                    Is.EqualTo("Add to cart"));

                Assert.That(cartBadge.Count, Is.EqualTo(0));
            });
        }
        [Test]
        public void RemoveMultipleProductsFromInventory()
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
                var badge =
                    d.FindElements(By.ClassName("shopping_cart_badge"));

                return badge.Count > 0 &&
                       badge[0].Text == "3";
            });

            Assert.Multiple(() =>
            {
                Assert.That(
                    driver.FindElement(
                        By.Id("remove-sauce-labs-backpack")).Text,
                    Is.EqualTo("Remove"));

                Assert.That(
                    driver.FindElement(
                        By.Id("remove-sauce-labs-bike-light")).Text,
                    Is.EqualTo("Remove"));

                Assert.That(
                    driver.FindElement(
                        By.Id("remove-sauce-labs-bolt-t-shirt")).Text,
                    Is.EqualTo("Remove"));
            });

            driver.FindElement(
                By.Id("remove-sauce-labs-backpack")).Click();

            wait.Until(d =>
                d.FindElements(
                    By.Id("add-to-cart-sauce-labs-backpack")).Count > 0);

            driver.FindElement(
                By.Id("remove-sauce-labs-bike-light")).Click();

            wait.Until(d =>
                d.FindElements(
                    By.Id("add-to-cart-sauce-labs-bike-light")).Count > 0);

            driver.FindElement(
                By.Id("remove-sauce-labs-bolt-t-shirt")).Click();

            wait.Until(d =>
                d.FindElements(
                    By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Count > 0);

            wait.Until(d =>
                d.FindElements(
                    By.ClassName("shopping_cart_badge")).Count == 0);

            Assert.Multiple(() =>
            {
                Assert.That(
                    driver.FindElement(
                        By.Id("add-to-cart-sauce-labs-backpack")).Text,
                    Is.EqualTo("Add to cart"));

                Assert.That(
                    driver.FindElement(
                        By.Id("add-to-cart-sauce-labs-bike-light")).Text,
                    Is.EqualTo("Add to cart"));

                Assert.That(
                    driver.FindElement(
                        By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Text,
                    Is.EqualTo("Add to cart"));

                Assert.That(
                    driver.FindElements(
                        By.ClassName("shopping_cart_badge")).Count,
                    Is.EqualTo(0));
            });
        }

        [Test]
        public void SortProductsByNameAToZ()
        {
            driver.FindElement(
                By.CssSelector("option[value='az']")).Click();

            var names =
                driver.FindElements(By.ClassName("inventory_item_name"))
                      .Select(p => p.Text)
                      .ToList();

            var sortedNames =
                names.OrderBy(n => n).ToList();

            Assert.That(names.Count, Is.EqualTo(6));
            Assert.That(names, Is.EqualTo(sortedNames));
        }

        [Test]
        public void SortProductsByNameZToA()
        {
            driver.FindElement(
                By.CssSelector("option[value='za']")).Click();

            var names =
                driver.FindElements(By.ClassName("inventory_item_name"))
                      .Select(p => p.Text)
                      .ToList();

            var sortedNames =
                names.OrderByDescending(n => n).ToList();

            Assert.That(names.Count, Is.EqualTo(6));
            Assert.That(names, Is.EqualTo(sortedNames));
        }

        [Test]
        public void SortProductsByPriceLowToHigh()
        {
            driver.FindElement(
                By.CssSelector("option[value='lohi']")).Click();

            var prices =
                driver.FindElements(By.ClassName("inventory_item_price"))
                      .Select(p =>
                          decimal.Parse(p.Text.Replace("$", ""), CultureInfo.InvariantCulture))
                      .ToList();

            var sortedPrices =
                prices.OrderBy(p => p).ToList();

            Assert.That(prices.Count, Is.EqualTo(6));
            Assert.That(prices, Is.EqualTo(sortedPrices));
        }

        [Test]
        public void SortProductsByPriceHighToLow()
        {
            driver.FindElement(
                By.CssSelector("option[value='hilo']")).Click();

            var prices =
                driver.FindElements(By.ClassName("inventory_item_price"))
                      .Select(p =>
                          decimal.Parse(p.Text.Replace("$", ""), CultureInfo.InvariantCulture))
                      .ToList();

            var sortedPrices =
                prices.OrderByDescending(p => p).ToList();

            Assert.That(prices.Count, Is.EqualTo(6));
            Assert.That(prices, Is.EqualTo(sortedPrices));
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