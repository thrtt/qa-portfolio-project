# Checkout test cases - SauceDemo

### CHTC-001 - Verify user can complete checkout with valid data

**Scenario:** CHS-001

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click "Add to cart" button
2. Click the shopping cart link
3. Click the checkout button
4. Fill the fields with valid data "First Name", "Last Name", "Zip/Postal Code"
5. Click "Continue" button
6. Click the "Finish" button
7. Verify the message "Thank you for your order!" is displayed

### CHTC-002 - Verify checkout fails with empty first name

**Scenario:** CHS-002

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click "Add to cart" button
2. Click the shopping cart link
3. Click the checkout button
4. Leave the "First Name" field empty
5. Fill the "Last Name" and "Zip/Postal Code" fields with valid data
6. Click "Continue" button
7. Verify the message "Error: First Name is required"

### CHTC-003 - Verify checkout fails with empty last name
**Scenario:** CHS-003

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click "Add to cart" button
2. Click the shopping cart link
3. Click the checkout button
4. Leave the "Last Name" field empty
5. Fill the "First Name" and "Zip/Postal Code" fields with valid data
6. Click "Continue" button
7. Verify the message "Error: Last Name is required"

### CHTC-004 - Verify checkout fails with empty postal code
**Scenario:** CHS-004

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click "Add to cart" button
2. Click the shopping cart link
3. Click the checkout button
4. Leave the "Zip/Postal Code" field empty
5. Fill the "First Name" and "Last Name" fields with valid data
6. Click "Continue" button
7. Verify the message "Error: Postal Code is required"

### CHTC-005 - Verify user can cancel checkout

**Scenario:** CHS-005

**Preconditions:**
- User is logged in with valid credentials
- User is on the inventory page

**Steps:**
1. Click the "Add to cart" button
2. Click the shopping cart link
3. Click the "Checkout" button
4. Fill the "First Name", "Last Name", and "Zip/Postal Code" fields with valid data
5. Click the "Continue" button
6. Click the "Cancel" button
7. Verify the user is redirected to the inventory page