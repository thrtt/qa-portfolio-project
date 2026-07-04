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
});
