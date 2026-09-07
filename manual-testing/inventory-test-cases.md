# Inventory Test Cases - SauceDemo

### ITC-001 - Verify products are displayed on the inventory page

**Scenario:** IS-001

**Preconditions:** 
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Observe the displayed products
2. Verify exactly six products are visible

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
5. Verify the product description is visible
6. Verify the "Add to cart" button is visible

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

### ITC-005 - Verify user can remove a single product from the inventory page

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Scenario:** IS-004

**Steps:**
1. Observe the displayed products
2. Click "Add to cart" on a product
3. Verify the cart badge displays "1"
4. Verify the product button changes to "Remove"
5. Click the "Remove" button on the inventory page
6. Verify the product button changes back to "Add to cart"
7. Verify the cart badge is no longer visible

### ITC-006 - Verify user can remove multiple products from the inventory page

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Scenario:** IS-004

**Steps:**
1. Observe the displayed products
2. Click "Add to cart" on three products
3. Verify the cart badge displays "3"
4. Verify each selected product button changes to "Remove"
5. Click the "Remove" button for each selected product on the inventory page
6. Verify each product button changes back to "Add to cart"
7. Verify the cart badge is no longer visible

### ITC-007 - Verify products can be sorted by ascending name

**Scenario:** IS-005

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click on the sort dropdown menu
2. Select "Name (A to Z)"
3. Verify the products are sorted alphabetically

### ITC-008 - Verify products can be sorted by descending name

**Scenario:** IS-005

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click on the sort dropdown menu
2. Select "Name (Z to A)"
3. Verify the products are sorted in reverse alphabetical order

### ITC-009 - Verify products can be sorted by ascending price

**Scenario:** IS-006

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click on the sort dropdown menu
2. Select "price (low to high)"
3. Verify the prices are sorted from low to high

### ITC-010 - Verify products can be sorted by descending price

**Scenario:** IS-006

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click on the sort dropdown menu
2. Select "price (high to low)"
3. Verify the prices are sorted from high to low

