import { test, expect } from "@playwright/test";

const routes = {
  home: "https://www.saucedemo.com/",
  inventory: "https://www.saucedemo.com/inventory.html",
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
  addToCartButton: 'button:has-text("Add to cart")',
  shoppingCartBadge: '[data-test="shopping-cart-badge"]',
  productSort: ".product_sort_container",
  cartIcon: '[data-test="shopping-cart-link"]',
  removeButton: '[data-test="remove-sauce-labs-backpack"]',
  continueShopping: '[data-test="continue-shopping"]',
  checkout: '[data-test="checkout"]',
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
    const title = page.locator(selectors.title);

    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(/inventory/);
    await expect(title).toHaveText("Products");
  });

  test("Verify login with invalid password", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.invalid);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with invalid username", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.invalid);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with empty username", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.empty);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText("Epic sadface: Username is required");
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with empty password", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.empty);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText("Epic sadface: Password is required");
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with empty username and password", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.empty);
    await page.locator(selectors.password).fill(passwords.empty);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText("Epic sadface: Username is required");
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with locked out user", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.locked);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText(
      "Epic sadface: Sorry, this user has been locked out.",
    );
    await expect(errorMessage).toBeVisible();
  });

  test("Verify login with invalid username and password", async ({ page }) => {
    const errorMessage = page.locator(selectors.error);

    await page.locator(selectors.username).fill(users.invalid);
    await page.locator(selectors.password).fill(passwords.invalid);
    await page.locator(selectors.loginButton).click();

    await expect(errorMessage).toHaveText(
      "Epic sadface: Username and password do not match any user in this service",
    );
    await expect(errorMessage).toBeVisible();
  });
});

test.describe("Inventory tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);

    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(/inventory/);
  });

  test("Verify all products are displayed", async ({ page }) => {
    await expect(page.locator(selectors.inventoryItem)).toHaveCount(6);
  });

  test("Verify product details are displayed correctly", async ({ page }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();

    await expect(
      firstProduct.locator(selectors.inventoryItemName),
    ).toBeVisible();
    await expect(
      firstProduct.locator(selectors.inventoryItemDescription),
    ).toBeVisible();
    await expect(
      firstProduct.locator(selectors.inventoryItemPrice),
    ).toBeVisible();
    await expect(firstProduct.locator(selectors.addToCartButton)).toBeVisible();
  });

  test("Verify user can add one product to the cart", async ({ page }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();
    const addCartButton = firstProduct.locator(selectors.addToCartButton);
    const removeButton = firstProduct.locator("//button[text()='Remove']");

    await addCartButton.click();

    await expect(removeButton).toBeVisible();
    await expect(page.locator(selectors.shoppingCartBadge)).toHaveText("1");
  });

  test("Verify user can add multiple products to the cart", async ({
    page,
  }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();
    const addCartButton = firstProduct.locator(selectors.addToCartButton);
    const removeButton = firstProduct.locator("//button[text()='Remove']");

    await addCartButton.click();

    await expect(removeButton).toBeVisible();
    await expect(page.locator(selectors.shoppingCartBadge)).toHaveText("1");
  });

  test("Verify user can remove a single product from the cart", async ({
    page,
  }) => {
    const firstProduct = page.locator(selectors.inventoryItem).first();
    const addCartButton = firstProduct.locator(selectors.addToCartButton);
    const removeButton = firstProduct.locator("//button[text()='Remove']");

    await addCartButton.click();
    await removeButton.click();

    await addCartButton.click();
    await removeButton.click();
  });

  test("Verify user can remove a multiple products from the cart", async ({
    page,
  }) => {
    const products = page.locator(selectors.inventoryItem);

    for (let i = 0; i < 2; i++) {
      await products.nth(i).locator(selectors.addToCartButton).click();
    }

    for (let i = 0; i < 2; i++) {
      await products.nth(i).locator("//button[text()='Remove']").click();
    }

    await expect(page.locator(selectors.shoppingCartBadge)).toHaveCount(0);
  });

  test("Verify products can be sorted by name (A to Z)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption({ value: "az" });

    const productName = page.locator(selectors.inventoryItemName);

    const actualNames = await productName.allTextContents();
    const expectedNames = [...actualNames].sort();

    await expect(actualNames).toEqual(expectedNames);
  });

  test("Verify products can be sorted by name (Z to A)", async ({ page }) => {
    await page.locator(selectors.productSort).selectOption({ value: "za" });

    const productName = page.locator(selectors.inventoryItemName);

    const actualNames = await productName.allTextContents();
    const expectedNames = [...actualNames].sort().reverse();

    await expect(actualNames).toEqual(expectedNames);
  });

  test("Verify products can be sorted by price (low to high)", async ({
    page,
  }) => {
    await page.locator(selectors.productSort).selectOption({ value: "lohi" });

    const productPrices = page.locator(selectors.inventoryItemPrice);

    const priceTexts = await productPrices.allTextContents();

    const actualPrices = priceTexts.map((price) =>
      Number(price.replace("$", "")),
    );

    const expectedPrices = [...actualPrices].sort((a, b) => a - b);

    expect(actualPrices).toEqual(expectedPrices);
  });

  test("Verify products can be sorted by price (high to low)", async ({
    page,
  }) => {
    await page.locator(selectors.productSort).selectOption({ value: "hilo" });

    const productPrices = page.locator(selectors.inventoryItemPrice);

    const priceTexts = await productPrices.allTextContents();

    const actualPrices = priceTexts.map((price) =>
      Number(price.replace("$", "")),
    );

    const expectedPrices = [...actualPrices].sort((a, b) => b - a);

    expect(actualPrices).toEqual(expectedPrices);
  });
});

test.describe("Cart tests", () => {
  test.beforeEach(async ({ page }) => {
    await page.goto(routes.home);

    await page.locator(selectors.username).fill(users.standard);
    await page.locator(selectors.password).fill(passwords.valid);
    await page.locator(selectors.loginButton).click();

    await expect(page).toHaveURL(/inventory/);
  });

  test("Verify one added product is displayed in cart", async ({ page }) => {
    const addProduct = page.locator(selectors.addToCartButton).first();
    await addProduct.click();

    const productInCart = page.locator(selectors.inventoryItemName);

    await page.locator(selectors.cartIcon).click();

    await expect(productInCart).toBeVisible();
  });

  test("Verify multiple added products are displayed in cart", async ({
    page,
  }) => {
    const productInCart = page.locator(selectors.inventoryItemName);

    const products = page.locator(selectors.inventoryItem);
    await products.nth(0).locator(selectors.addToCartButton).click();
    await products.nth(1).locator(selectors.addToCartButton).click();

    await page.locator(selectors.cartIcon).click();

    await expect(productInCart).toHaveCount(2);
  });

  test("Verify user can continue shopping from the cart", async ({ page }) => {
    await page.locator(selectors.addToCartButton).first().click();

    await page.locator(selectors.cartIcon).click();

    await page.locator(selectors.continueShopping).click();

    await expect(page).toHaveURL(routes.inventory);
  });

  test("Verify user can complete checkout", async ({ page }) => {
    const completeHeader = page.locator(selectors.completeHeader);

    await page.locator(selectors.addToCartButton).first().click();
    await page.locator(selectors.cartIcon).click();

    await page.locator(selectors.checkout).click();

    await page.locator('[data-test="firstName"]').fill('John');
    await page.locator('[data-test="lastName"]').fill('Smith');
    await page.locator('[data-test="postalCode"]').fill('85412');

    await page.locator('[data-test="continue"]').click();

    await page.locator('[data-test="finish"]').click()

    await expect(completeHeader).toHaveText("Thank you for your order!");
  });
});
