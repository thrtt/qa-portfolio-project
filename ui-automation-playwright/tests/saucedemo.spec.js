import { test, expect } from "@playwright/test";

const routes = {
  home: "https://www.saucedemo.com/",
  inventory: "https://www.saucedemo.com/inventory.html",
  cart: "https://www.saucedemo.com/cart.html",
};

const selectors = {
  username: '[data-test="username"]',
  password: '[data-test="password"]',
  loginButton: '[data-test="login-button"]',
  title: '[data-test="title"]',
  error: '[data-test="error"]',
  inventoryItem: '[data-test="inventory-item"]',
  inventoryItemName: '[data-test="inventory-item-name"]',
  inventoryItemDescription: '[data-test="inventory-item-desc"]',
  inventoryItemPrice: '[data-test="inventory-item-price"]',
  inventoryItemImage: ".inventory_item_img img",
  addToCartButton: 'button:has-text("Add to cart")',
  removeButton: 'button:has-text("Remove")',
  shoppingCartBadge: '[data-test="shopping-cart-badge"]',
  shoppingCartLink: '[data-test="shopping-cart-link"]',
  productSort: '[data-test="product-sort-container"]',
  continueShopping: '[data-test="continue-shopping"]',
  checkout: '[data-test="checkout"]',
  firstName: '[data-test="firstName"]',
  lastName: '[data-test="lastName"]',
  postalCode: '[data-test="postalCode"]',
  continueCheckout: '[data-test="continue"]',
  finish: '[data-test="finish"]',
  cancel: '[data-test="cancel"]',
  completeHeader: '[data-test="complete-header"]',
};

const users = {
  standard: "standard_user",
  locked: "locked_out_user",
  invalid: "wrong_user",
  empty: "",
};

const passwords = {
  valid: "secret_sauce",
  invalid: "wrong_password",
  empty: "",
};

test.describe("Login tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);
  });

  test("Verify login with valid credentials", async ({ page }) => {
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(routes.inventory);
    await expect(page.locator(selectors.title)).toHaveText("Products");
  });

  test("Verify login with invalid password", async ({ page }) => {
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.invalid);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with invalid username", async ({ page }) => {
    await page.locator(selectors.username).fill(users.invalid);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with empty username", async ({ page }) => {
    await page.locator(selectors.username).fill(users.empty);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Username is required",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with empty password", async ({ page }) => {
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.empty);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Password is required",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with empty username and password", async ({ page }) => {
    await page.locator(selectors.username).fill(users.empty);
    await page.locator(selectors.password).fill(passwords.empty);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Username is required",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with locked out user", async ({ page }) => {
    await page.locator(selectors.username).fill(users.locked);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Sorry, this user has been locked out.",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify login with invalid username and password", async ({ page }) => {
    await page.locator(selectors.username).fill(users.invalid);
    await page.locator(selectors.password).fill(passwords.invalid);
    await page.locator(selectors.loginButton).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(page).toHaveURL(routes.home);
  });

  test("Verify password field masks entered characters", async ({ page }) => {
    const passwordField = page.locator(selectors.password);

    await passwordField.fill(passwords.valid);

    await expect(passwordField).toHaveAttribute("type", "password");
  });
});

test.describe("Inventory tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(routes.inventory);
  });

  test("Verify all products are displayed", async ({ page }) => {
    await expect(page.locator(selectors.inventoryItem)).toHaveCount(6);
  });

  test("Verify product details are displayed correctly", async ({ page }) => {
    const products = page.locator(selectors.inventoryItem);

    await expect(products).toHaveCount(6);

    for (const product of await products.all()) {
      await expect(product.locator(selectors.inventoryItemName)).toBeVisible();
      await expect(product.locator(selectors.inventoryItemDescription)).toBeVisible();
      await expect(product.locator(selectors.inventoryItemPrice)).toBeVisible();
      await expect(product.locator(selectors.inventoryItemImage)).toBeVisible();
      await expect(product.locator(selectors.addToCartButton)).toBeVisible();
    }
  });

  test("Verify user can add one product to the cart", async ({ page }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();
    const selectedProductName = await firstProduct
      .locator(selectors.inventoryItemName)
      .textContent();

    await firstProduct.locator(selectors.addToCartButton).click();
    await expect(page.locator(selectors.shoppingCartBadge)).toHaveText("1");
    await page.locator(selectors.shoppingCartLink).click();

    await expect(page).toHaveURL(routes.cart);
    await expect(page.locator(selectors.inventoryItemName)).toHaveText(
      selectedProductName,
    );
  });

  test("Verify user can add multiple products to the cart", async ({ page }) => {
    const products = page.locator(selectors.inventoryItem);
    const selectedProductNames = await page
      .locator(selectors.inventoryItemName)
      .evaluateAll((elements) =>
        elements.slice(0, 3).map((element) => element.textContent),
      );

    await products.nth(0).locator(selectors.addToCartButton).click();
    await products.nth(1).locator(selectors.addToCartButton).click();
    await products.nth(2).locator(selectors.addToCartButton).click();

    await expect(page.locator(selectors.shoppingCartBadge)).toHaveText("3");
    await page.locator(selectors.shoppingCartLink).click();
    await expect(page.locator(selectors.inventoryItemName)).toHaveText(
      selectedProductNames,
    );
  });

  test("Verify products can be sorted by name (A to Z)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption("az");

    const actualNames = await page
      .locator(selectors.inventoryItemName)
      .allTextContents();
    const expectedNames = [...actualNames].sort((first, second) =>
      first.localeCompare(second),
    );

    expect(actualNames).toEqual(expectedNames);
  });

  test("Verify products can be sorted by name (Z to A)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption("za");

    const actualNames = await page
      .locator(selectors.inventoryItemName)
      .allTextContents();
    const expectedNames = [...actualNames]
      .sort((first, second) => first.localeCompare(second))
      .reverse();

    expect(actualNames).toEqual(expectedNames);
  });

  test("Verify products can be sorted by price (low to high)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption("lohi");

    const priceTexts = await page
      .locator(selectors.inventoryItemPrice)
      .allTextContents();
    const actualPrices = priceTexts.map((price) =>
      Number(price.replace("$", "")),
    );
    const expectedPrices = [...actualPrices].sort((first, second) => first - second);

    expect(actualPrices).toEqual(expectedPrices);
  });

  test("Verify products can be sorted by price (high to low)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption("hilo");

    const priceTexts = await page
      .locator(selectors.inventoryItemPrice)
      .allTextContents();
    const actualPrices = priceTexts.map((price) =>
      Number(price.replace("$", "")),
    );
    const expectedPrices = [...actualPrices].sort((first, second) => second - first);

    expect(actualPrices).toEqual(expectedPrices);
  });
});

test.describe("Cart tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(routes.inventory);
  });

  test("Verify one added product is displayed in the cart", async ({ page }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();
    const selectedProductName = await firstProduct
      .locator(selectors.inventoryItemName)
      .textContent();

    await firstProduct.locator(selectors.addToCartButton).click();
    await page.locator(selectors.shoppingCartLink).click();

    await expect(page).toHaveURL(routes.cart);
    await expect(page.locator(selectors.inventoryItem)).toHaveCount(1);
    await expect(page.locator(selectors.inventoryItemName)).toHaveText(
      selectedProductName,
    );
  });

  test("Verify multiple added products are displayed in the cart", async ({ page }) => {
    const products = page.locator(selectors.inventoryItem);
    const selectedProductNames = await page
      .locator(selectors.inventoryItemName)
      .evaluateAll((elements) =>
        elements.slice(0, 3).map((element) => element.textContent),
      );

    await products.nth(0).locator(selectors.addToCartButton).click();
    await products.nth(1).locator(selectors.addToCartButton).click();
    await products.nth(2).locator(selectors.addToCartButton).click();
    await page.locator(selectors.shoppingCartLink).click();

    await expect(page).toHaveURL(routes.cart);
    await expect(page.locator(selectors.inventoryItem)).toHaveCount(3);
    await expect(page.locator(selectors.inventoryItemName)).toHaveText(
      selectedProductNames,
    );
  });

  test("Verify user can remove one product from the cart", async ({ page }) => {
    await page.locator(selectors.addToCartButton).first().click();
    await page.locator(selectors.shoppingCartLink).click();
    await page.locator(selectors.removeButton).click();

    await expect(page.locator(selectors.inventoryItem)).toHaveCount(0);
    await expect(page.locator(selectors.shoppingCartBadge)).toHaveCount(0);
  });

  test("Verify user can remove multiple products from the cart", async ({ page }) => {
    const products = page.locator(selectors.inventoryItem);

    await products.nth(0).locator(selectors.addToCartButton).click();
    await products.nth(1).locator(selectors.addToCartButton).click();
    await products.nth(2).locator(selectors.addToCartButton).click();
    await page.locator(selectors.shoppingCartLink).click();

    const removeButtons = page.locator(selectors.removeButton);
    await removeButtons.nth(0).click();
    await removeButtons.nth(0).click();
    await removeButtons.nth(0).click();

    await expect(page.locator(selectors.inventoryItem)).toHaveCount(0);
    await expect(page.locator(selectors.shoppingCartBadge)).toHaveCount(0);
  });

  test("Verify user can continue shopping from the cart", async ({ page }) => {
    await page.locator(selectors.addToCartButton).first().click();
    await page.locator(selectors.shoppingCartLink).click();
    await page.locator(selectors.continueShopping).click();

    await expect(page).toHaveURL(routes.inventory);
  });
});

test.describe("Checkout tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);
    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await page.locator(selectors.addToCartButton).first().click();
    await page.locator(selectors.shoppingCartLink).click();
    await page.locator(selectors.checkout).click();

    await expect(page).toHaveURL(/checkout-step-one\.html/);
  });

  test("Verify user can complete checkout with valid data", async ({ page }) => {
    await page.locator(selectors.firstName).fill("Nikolay");
    await page.locator(selectors.lastName).fill("Matev");
    await page.locator(selectors.postalCode).fill("33353");
    await page.locator(selectors.continueCheckout).click();
    await page.locator(selectors.finish).click();

    await expect(page.locator(selectors.completeHeader)).toHaveText(
      "Thank you for your order!",
    );
  });

  test("Verify checkout fails with empty first name", async ({ page }) => {
    await page.locator(selectors.firstName).fill("");
    await page.locator(selectors.lastName).fill("Matev");
    await page.locator(selectors.postalCode).fill("33353");
    await page.locator(selectors.continueCheckout).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Error: First Name is required",
    );
  });

  test("Verify checkout fails with empty last name", async ({ page }) => {
    await page.locator(selectors.firstName).fill("Nikolay");
    await page.locator(selectors.lastName).fill("");
    await page.locator(selectors.postalCode).fill("33353");
    await page.locator(selectors.continueCheckout).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Error: Last Name is required",
    );
  });

  test("Verify checkout fails with empty postal code", async ({ page }) => {
    await page.locator(selectors.firstName).fill("Nikolay");
    await page.locator(selectors.lastName).fill("Matev");
    await page.locator(selectors.postalCode).fill("");
    await page.locator(selectors.continueCheckout).click();

    await expect(page.locator(selectors.error)).toHaveText(
      "Error: Postal Code is required",
    );
  });

  test("Verify user can cancel checkout", async ({ page }) => {
    await page.locator(selectors.firstName).fill("Nikolay");
    await page.locator(selectors.lastName).fill("Matev");
    await page.locator(selectors.postalCode).fill("33353");
    await page.locator(selectors.continueCheckout).click();
    await page.locator(selectors.cancel).click();

    await expect(page).toHaveURL(routes.inventory);
  });
});
