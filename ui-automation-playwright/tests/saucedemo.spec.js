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
    const expextedNames = [...actualNames].sort();

    await expect(actualNames).toEqual(expextedNames);
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
