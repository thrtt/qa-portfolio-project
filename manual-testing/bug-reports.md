# Bug Reports - SauceDemo

---

### BUG-001 - Unprofessional wording in login error messages

**Related Test Cases:** TC-002, TC-003, TC-004, TC-005, TC-006, TC-007

**Steps to Reproduce:**
1. Open the app
2. Trigger any login validation error by using invalid, empty, or locked out user credentials
3. Observe the displayed error message

**Actual Result:**
Login error messages contain the informal phrase "Epic sadface", for example:
- "Epic sadface: Username is required"
- "Epic sadface: Password is required"
- "Epic sadface: Username and password do not match any user in this service"
- "Epic sadface: Sorry, this user has been locked out."

**Expected Result:**
System displays clear and professional login error messages without informal expressions.

**Severity:** Low

**Priority:** Medium