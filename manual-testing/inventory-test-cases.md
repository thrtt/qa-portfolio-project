# Inventory Test Cases - SauceDemo

### ITC-001 - Verify products are displayed on the inventory page

**Scenario:** IS-001

**Preconditions:** 
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Observe the displayed products
2. Verify all products are visible

### ITC-002 - Verify product details are displayed correctly

**Scenario:** IS-002

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Observe the displayed products
2. Verify the product name is visible
3. Verify the product price is visible
4. Verify the product image is visible
5. Verify the "add to cart" button is visible

### ITC-003 - Verify user can add one product to the cart

**Scenario:** IS-003

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Observe the displayed products
2. Click "add to cart" on the product
3. Verify the cart badge displays "1"
4. Open the shopping cart
5. Verify the selected product is displayed in the cart

### ITC-004 - Verify user can add multiple products to the cart

**Scenario:** IS-003

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Observe the displayed products
2. Click "add to cart" on multiple products
3. Verify the cart badge displays the correct number of added products
4. Open the shopping cart
5. Verify the selected products are displayed in the cart

### ITC-005 - Verify user can remove a single product from the cart

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Scenario:** IS-004

**Steps:**
1. Observe the displayed products
2. Click "Add to cart" on a product
3. Verify the cart badge displays "1"
4. Open the shopping cart
5. Verify the selected product is displayed in the cart
6. Click the "Remove" button
7. Verify the selected product is no longer displayed in the cart
8. Verify the cart badge is no longer visible

### ITC-006 - Verify user can remove a multiple products from the cart

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Scenario:** IS-004

**Steps:**
1. Observe the displayed products
2. Click "add to cart" on multiple product
3. Verify the cart badge displays "3"
4. Open the shopping cart
5. Verify the selected products are displayed in the cart
6. Click the "Remove" button for each selected product
7. Verify the selected products are no longer displayed in the cart
8. Verify the cart badge is no longer visible
