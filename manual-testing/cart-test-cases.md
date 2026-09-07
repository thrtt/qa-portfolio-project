### CTC-001 - Verify one added product is displayed in cart

**Scenario:** CS-001

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Add one product to the cart
2. Click on the cart icon
3. Verify the cart page is displayed
4. Verify exactly one product is displayed in the cart
5. Verify the displayed product is the product that was added

### CTC-002 - Verify multiple added products are displayed in cart

**Scenario:** CS-001

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Add three products to the cart
2. Click on the cart icon
3. Verify the cart page is displayed
4. Verify exactly three products are displayed in the cart
5. Verify the displayed products are the products that were added

### CTC-003 - Verify user can remove one product from the cart

**Scenario:** CS-002

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Add one product to the cart
2. Click on the cart icon
3. Click on the "Remove" button
4. Verify the removed product is no longer displayed in the cart
5. Verify the cart badge is no longer visible

### CTC-004 - Verify user can remove multiple products from the cart

**Scenario:** CS-002

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Add three products to the cart
2. Click on the cart icon
3. Click the "Remove" button for each added product
4. Verify the removed products are no longer displayed in the cart
5. Verify the cart badge is no longer visible

### CTC-005 - Verify user can continue shopping from the cart

**Scenario:** CS-003

**Preconditions:**
- User is logged in with valid credentials
- User has added at least one product to the cart
- User is on the cart page

**Steps:**
1. Click the "Continue Shopping" button
2. Verify the user is redirected to the inventory page

### CTC-006 - Verify user can proceed to checkout

**Scenario:** CS-004

**Preconditions:**
- User is logged in with valid credentials
- User has added at least one product to the cart
- User is on the cart page

**Steps:**
1. Click the "Checkout" button
2. Verify the checkout information page is displayed
3. Verify the "First Name", "Last Name", and "Zip/Postal Code" fields are visible


