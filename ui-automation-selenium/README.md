# UI Automation - Selenium

Automated SauceDemo UI tests using C#, .NET 8, NUnit and Selenium WebDriver.

## Coverage

28 tests cover login, logout and validation errors, product information and sorting,
adding and removing products, cart navigation, and opening checkout.
Checkout coverage verifies the customer information form; it does not submit an order.

## Requirements

- .NET 8 SDK or a newer SDK with the .NET 8 runtime
- Google Chrome
- Internet access to SauceDemo and Selenium Manager's driver downloads

## Run

From this directory:

```powershell
dotnet test SauceDemo.Selenium.Tests/SauceDemo.Selenium.Tests.sln
```

To run without visible browser windows and save a test report:

```powershell
$env:HEADLESS = '1'
dotnet test SauceDemo.Selenium.Tests/SauceDemo.Selenium.Tests.sln --logger "trx;LogFileName=regression.trx"
```

Reports are written to the test project's `TestResults` directory.
Each test creates its own browser session and disposes it afterward.
Price parsing uses invariant culture so sorting tests also work with Bulgarian regional settings.

Failed tests attach a browser screenshot to the test results.
