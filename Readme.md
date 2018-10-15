This code requires .net core to be installed on your machine to run. I tested
it on a Mac, should run on Linux and Windows with either Visual Studio (for Mac/Linux
or the main Windows release) or just the commandline SDK.

You can run the unit tests by using `dotnet test`:
```
Jamess-MacBook:UnitTests moof$ dotnet test
Build started, please wait...
Build completed.

Test run for /Users/moof/projects/MortgageCalculator/UnitTests/bin/Debug/netcoreapp2.1/UnitTests.dll(.NETCoreApp,Version=v2.1)
Microsoft (R) Test Execution Command Line Tool Version 15.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

Total tests: 10. Passed: 10. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.7576 Seconds
```

You can run tests of the end points by:

In one terminal:
```
Jamess-MacBook:MortgageCalculator moof$ cd MortgageCalculator
Jamess-MacBook:MortgageCalculator moof$ dotnet run
Using launch settings from /Users/moof/projects/MortgageCalculator/MortgageCalculator/Properties/launchSettings.json...
: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using '/Users/moof/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
Hosting environment: Development
Content root path: /Users/moof/projects/MortgageCalculator/MortgageCalculator
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

In Another Terminal:

Test Payment-Amount:
```
curl --insecure --request GET https://localhost:5001/payment-amount --header "Content-Type: application/json" --data '{ "AskingPrice": 750000.00, "DownPayment": 158000.00, "PaymentSchedule": "monthly", "AmortizationPeriod": 240  }'

{"paymentAmount":3137.0251267506815}
```

Test Mortgage-amount:
```
url --insecure --request GET https://localhost:5001/mortgage-amount --header "Content-Type: application/json" --data '{ "PaymentAmount": 3137.0251267506815, "DownPayment": 158000.00, "PaymentSchedule": "monthly", "AmortizationPeriod": 240  }'
{"paymentAmount":750000.0}
```

Test set interest rate:
```
curl --insecure --request PATCH https://localhost:5001/interest-rate --header "Content-Type: application/json"  --data '{"NewRate": 0.03 }'
{"oldRate":0.025,"newRate":0.03}
```


Areas for improvement:
- Better Unit tests, more coverage, cover exceptions 
- Custom exception handler that translates to custom error page (really translate to a  json error response object) - right now the default behaviour is an ASP.net template that is meant to be displayed in a web browser


