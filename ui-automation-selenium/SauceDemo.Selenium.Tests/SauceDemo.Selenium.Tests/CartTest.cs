using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Selenium.Tests
{
    public class CartTest
    {
        private IWebDriver driver;

        private readonly string BaseUrl = "https://www.saucedemo.com/";
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
        public void AddItemToCartTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            Assert.That(driver.Url, Is.EqualTo(CartUrl));
            Assert.That(driver.FindElement(By.ClassName("cart_item")).Displayed, Is.True);
        }

        [Test]
        public void AddMultipleItemsToCartTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            Assert.That(driver.Url, Is.EqualTo(CartUrl));
            Assert.That(driver.FindElements(By.ClassName("cart_item")).Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveItemFromCartTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
            Assert.That(driver.FindElements(By.ClassName("cart_item")).Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveMultipleItemsFromCartTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("remove-sauce-labs-bike-light")).Click();
            Assert.That(driver.FindElements(By.ClassName("cart_item")).Count, Is.EqualTo(0));
        }

        [Test]
        public void CheckoutTest()
        {
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            driver.FindElement(By.Id("checkout")).Click();
            Assert.That(driver.Url, Is.EqualTo("https://www.saucedemo.com/checkout-step-one.html"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
