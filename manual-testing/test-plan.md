# Test Plan - SauceDemo

## 1. Objective

The objective of this test plan is to verify that the core functionalities of the SauceDemo web application work as expected through manual testing.

## 2. Scope

### In Scope
- Login functionality
- Product inventory page
- Add to cart / Remove from cart
- Cart functionality
- Checkout process
- Logout

### Out of Scope
- Performance testing
- Security testing
- Automation testing

## 3. Test Strategy

Manual functional testing will be performed.

Testing will include:
- Positive scenarios (valid inputs)
- Negative scenarios (invalid inputs)
- Boundary conditions (empty fields, edge cases)

## 4. Test Environment

- Application: SauceDemo
- URL: https://www.saucedemo.com/
- Browser: Mozilla Firefox Version 150.0.1 (64-bit)
- OS: Windows 11 Pro

## 5. Test Data

The following test users will be used:

- standard_user
- locked_out_user
- problem_user
- performance_glitch_user

Password for all users: `secret_sauce`

## 6. Entry Criteria

- Application is accessible
- Test environment is ready

## 7. Exit Criteria

- All planned test cases are executed
- All critical and high severity defects are reported

## 8. Deliverables

- Test Plan
- Test Scenarios
- Test Cases
- Bug Reports
- Test Summary Report

## 9. Risks

- Limited test environment (single browser)
- Demo application may not reflect real-world complexity
- Limited time for testing may affect coverage