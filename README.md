# API_TASK

This project is aimed to test API endpoint under https://api-test.afterpay.dev/
The project contains two folders, one of them is API_Task which has all the code for testing of the API endpoint and folder called
images which has screenshots used in report.

In order to run successfully the script following dependencies are needed:

-System.Collections.Generic;
-System.Text;
-System.Net.Http;
-System.Net.Http.Headers;
-System.Threading.Tasks;
-Newtonsoft.Json;
-Newtonsoft.Json.Linq;
-NUnit.Framework;

Nunit Framework and Newtonsoft should be installed and added to the project.
In Visual Studio you can add package by going to Tools -> NuGet Packet Manager -> Manage NuGet Packages for Solutions like in figures below.
![Installing Packages](https://github.com/ademilua/API_TASK/blob/master/images/2.PNG)

After installing necessary packages you can run the code in Test Suite by following link https://github.com/ademilua/API_TASK/blob/master/API_Task/API_Task/Test_Suite.cs

If you faced with build issue then you need to right click on the project name then go to property and untick Prefer 32-bit.
After that you might need to also change the CPU architecture to x64. In order to do that go to Test -> Test Settings -> Default processor archictecture -> x64 like in figure below.
![Processor architecture](https://github.com/ademilua/API_TASK/blob/master/images/8.PNG)

Test Suites has 7 Test Cases which covers Positive and Negative cases related to The API based on specification document.
Test Cases are following:
1. First test case verifies positive case of getting HTTP 200 and isValid attribute having True Value. 
It's tested by sending valid Token and Valid IBAN code to correct API.
2. Second test case verifies IBAN checking by sending valid Token with invalid IBAN. In that case, HTTP 200 is returned but isValid attribute will have False value.
3. Third test case verifies negative case of getting Unathorized HTTP error with 401 HTTP code and with message attribute returned having "Authorization has been denied for this request." value. 
It's tested by sending request with missing Token and correct IBAN.
4. Fourth test case is also verifying negative case of getting Unathorized HTTP error with 401 HTTP code and with message attribute returned having "Authorization has been denied for this request." value.
However, in this test case the request is initiated with invalid Token instead of missing one. IBAN values is still correct. 
5. Fifth test case verifies negative case of getting Bad Request HTTP error with 400 HTTP code and with type attribute returned having "BusinessError" value. It is tested by sending incorrect IBAN, that has shorter length and correct Token. 
6. Sixth test case also verifies negative case of getting Bad Request HTTP error with 400 HTTP code and with type attribute returned having "BusinessError" value. 
However, in this test case the IBAN is missing and Token is correct. 
7. The last, seventh test case verifies that bank account with format different from IBAN is not accepted as legit format. In this test case, credit card is generated by using online tool in following link formathttps://www.bincodes.com/bank-creditcard-generator/
It also verifies negative case of getting Bad Request HTTP error with 400 HTTP code and with type attribute returned having "BusinessError" value. 

All test cases are passed like displayed in figure below: 

![Passed Test Cases](https://github.com/ademilua/API_TASK/blob/master/images/3.PNG)

NOTES:

Estonian IBAN is generated EE474554454545454545 by using the following online tool https://bank.codes/iban/generate/estonia/ and tested by using POSTMAN tool. Based on specification document the IBAN supposed to be invalid and return False but instead it returned True value like in figures below.

![POSTMAN](https://github.com/ademilua/API_TASK/blob/master/images/5.PNG)
![List Of Countries](https://github.com/ademilua/API_TASK/blob/master/images/6.PNG)

In addition, “riskCheckMessages” are not tested because of lack of information in specification document. 
Since it was not specified what parameters are affecting those values in “riskCheckMessages”.


