# Logout Test Cases - SauceDemo

### LTC-001 - Verify a logged-in user can log out

**Scenario:** LS-001

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Open the application menu
2. Click the "Logout" link
3. Verify the login page is displayed
4. Verify the username, password, and Login controls are visible

**Expected Result:**
The user is logged out and redirected to the login page

**Priority:** High

### LTC-002 - Verify a logged-out user cannot access the inventory page

**Scenario:** LS-002

**Preconditions:**
- User has logged out
- User is on the login page

**Steps:**
1. Enter `https://www.saucedemo.com/inventory.html` in the browser address bar
2. Press Enter
3. Observe the displayed page and message

**Expected Result:**
The user remains on the login page and an authorization error message is displayed

**Priority:** High
