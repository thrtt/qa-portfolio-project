using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class InventoryTest
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

            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
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
            foreach (var product in products)
            {
                var name = product.FindElement(By.ClassName("inventory_item_name")).Text;
                var description = product.FindElement(By.ClassName("inventory_item_desc")).Text;
                var price = product.FindElement(By.ClassName("inventory_item_price")).Text;
                Assert.Multiple(() =>
                {
                    Assert.That(name, Is.Not.Null.And.Not.Empty);
                    Assert.That(description, Is.Not.Null.And.Not.Empty);
                    Assert.That(price, Is.Not.Null.And.Not.Empty);
                });

            }
        }

        [Test]
        public void AddProductToCart()
        {
            var firstProduct = driver.FindElement(By.ClassName("inventory_item"));
            var addToCartButton = firstProduct.FindElement(By.ClassName("btn_inventory"));
            addToCartButton.Click();
            var cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(cartBadge.Text, Is.EqualTo("1"));
        }
        [Test]
        public void AddMultipleProductsToCart()
        {
            var products = driver.FindElements(By.ClassName("inventory_item"));

            for (int i = 0; i < 3; i++)
            {
                var addToCartButton =
                    products[i].FindElement(By.ClassName("btn_inventory"));

                addToCartButton.Click();
            }

            var cartBadge =
                driver.FindElement(By.ClassName("shopping_cart_badge"));

            Assert.That(cartBadge.Text, Is.EqualTo("3"));
        }

        [Test]
        public void SortProductsByPriceLowToHigh()
        {   var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            sortDropdown.Click();

            var lowToHighOption = driver.FindElement(By.CssSelector("option[value='lohi']"));
            lowToHighOption.Click();

            var products = driver.FindElements(By.ClassName("inventory_item_price"));

            var prices = products.Select(p => decimal.Parse(p.Text.Replace("$", ""))).ToList();

            var sortedPrices = prices.OrderBy(p => p).ToList();

            Assert.That(prices, Is.EqualTo(sortedPrices));
        }

        [Test]
        public void SortProductsByPriceHighToLow()
        {
            var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            sortDropdown.Click();
            var highToLowOption = driver.FindElement(By.CssSelector("option[value='hilo']"));
            highToLowOption.Click();
            var products = driver.FindElements(By.ClassName("inventory_item_price"));
            var prices = products.Select(p => decimal.Parse(p.Text.Replace("$", ""))).ToList();
            var sortedPrices = prices.OrderByDescending(p => p).ToList();
            Assert.That(prices, Is.EqualTo(sortedPrices));
        }

        [Test]
        public void SortProductsByNameAToZ()
        {
            var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            sortDropdown.Click();
            var aToZOption = driver.FindElement(By.CssSelector("option[value='az']"));
            aToZOption.Click();
            var products = driver.FindElements(By.ClassName("inventory_item_name"));
            var names = products.Select(p => p.Text).ToList();
            var sortedNames = names.OrderBy(n => n).ToList();
            Assert.That(names, Is.EqualTo(sortedNames));
        }

        [Test]
        public void SortProductsByNameZToA()
        {
            var sortDropdown = driver.FindElement(By.ClassName("product_sort_container"));
            sortDropdown.Click();
            var zToAOption = driver.FindElement(By.CssSelector("option[value='za']"));
            zToAOption.Click();
            var products = driver.FindElements(By.ClassName("inventory_item_name"));
            var names = products.Select(p => p.Text).ToList();
            var sortedNames = names.OrderByDescending(n => n).ToList();
            Assert.That(names, Is.EqualTo(sortedNames));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
    