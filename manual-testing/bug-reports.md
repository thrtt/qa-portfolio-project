# Bug Reports - SauceDemo

---

### BUG-001 - Unprofessional error message on login page

**Related Test Case:** TC-004 - Login with empty username

**Steps to Reproduce:**
1. Open the app
2. Leave username field empty
3. Enter any password
4. Click Login button

**Actual Result:** 
Error message "Epic sadface: Username is required" is displayed

**Expected Result:**
System displays a clear and professional error message indicating that the username is required

**Severity:** Low

**Priority:** Medium

---

### BUG-002 - Inconsistent tone in login error messages

**Related Test Case:** TC-007 - Login with locked out user

**Steps to Reproduce:**
1. Open the app
2. Enter locked out username in username field
3. Enter valid password in password field
4. Click Login button

**Actual Result:** 
Error message containing the phrase "Epic sadface" is displayed on the login page

**Expected Result:**
System displays a clear and professional error message without informal expressions

**Severity:** Low

**Priority:** Medium