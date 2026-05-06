### TC-001 - Login page with valid credentials

**Scenario:** TS-001
**Preconditions:** User is on the login page

**Steps:**

1. Enter valid username in username field
2. Enter valid password in password field
3. Click Login button

**Test Data:**

- username: standard_user
- password: secret_sauce

**Expected Result:**
User is successfully logged in and redirected to the inventory page

**Priority:** High

---

### TC-002 - Login with invalid password

**Scenario:** TS-002
**Preconditions:** User is on the login page

**Steps:**

1. Enter valid username in username field
2. Enter invalid password in password field
3. Click Login button

**Test Data:**

- username: standard_user
- password: wrong_password

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High

---

### TC-003 - Login with invalid username

**Scenario:** TS-003
**Preconditions:** User is on the login page

**Steps:**

1. Enter invalid username in username field
2. Enter valid password in password field
3. Click Login button

**Test Data:**

- username: wrong_user
- password: secret_sauce

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High

---

### TC-004 - Login with empty username

**Scenario:** TS-004
**Preconditions:** User is on the login page

**Steps:**

1. Leave username field empty
2. Enter valid password in password field
3. Click Login button

**Test Data:**

- username: (empty)
- password: secret_sauce

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High

---

### TC-005 - Login with empty password

**Scenario:** TS-005
**Preconditions:** User is on the login page

**Steps:**

1. Enter valid username in username field
2. Leave password field empty
3. Click Login button

**Test Data:**

- username: standard_user
- password: (empty)

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High

---

### TC-006 - Login with empty username and password

**Scenario:** TS-006
**Preconditions:** User is on the login page

**Steps:**

1. Leave username field empty
2. Leave password field empty
3. Click Login button

**Test Data:**

- username: (empty)
- password: (empty)

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High

---

### TC-007 - Login with locked out user
**Scenario:** TS-007
**Preconditions:** User is on the login page

**Steps:**

1. Enter locked out username in username field
2. Enter valid password in password field
3. Click Login button

**Test Data:**

- username: locked_out_user
- password: secret_sauce

**Expected Result:**
System displays an error message indicating the user is locked out and user remains on the login page

**Priority:** High

---

### TC-008 - Login with invalid username and password

**Scenario:** TS-008
**Preconditions:** User is on the login page

**Steps:**

1. Enter invalid username in username field
2. Enter invalid password in password field
3. Click Login button

**Test Data:**

- username: wrong_user
- password: wrong_password

**Expected Result:**
System displays an error message and user remains on the login page

**Priority:** High