## Description
This .Net automation test framework contains two projects: Calculator web tests and Calculator API tests. The web test framework is built with Specflow and Page Object model design pattern while API test framework is created without the BDD layer.

## Test Scenarios
- Verify the addition of 2 positive numbers                               
- Verify the addition of 2 negative numbers                               
- Verify the addition of 1 positive number and 1 negative number          
- Verify the substraction of 2 positive numbers                           
- Verify the substraction of 2 positive numbers                           
- Verify the substraction of 2 positive numbers                           
- Verify the the multiplication of 2 positive numbers                     
- Verify the the multiplication of 2 negative numbers                     
- Verify the the multiplication of 1 positive number and 1 negative number
- Verify the division of 2 positive numbers                               
- Verify the division of 2 negative numbers                               
- Verify the division of 1 positive number and 1 negative number          
- Verify the division of zero by any number                               

## Issues
1. Web calculator: All negative numbers input are treated as positive numbers so the results are not reliable if left number and/or right number are/is negative. What needs to be highlighted is -1 * -1 = 1 which looks correct, but it is actually false postive.
2. Web calculator: When the page is first loaded, the operator has to be reselected, otherwise, operator value in the request body is "" even if it is displayed as "+" by default.
3. Web calculator: The left number and right number fields can only hold maximum 3 digits while results text field can only hold 5 digits, which will cause a problem when results could be 6 digits. E.g. 999*999 = 998001 but it is displayed as 99800.
4. Web calculator: An error message should be displayed when calculating the division of a number by zero, instead of 0 in the result field.
5. Api calculator: 500 error is returned when calculating the division of a number by zero, which needs to be handled better, 400 error should be expected.
6. Api calculator: 500 error is returned when the left number or right number is floating number, which needs to be handled better, 400 error should be expected.
