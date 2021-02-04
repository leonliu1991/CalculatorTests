Feature: CalculatorWebTests

@PostiveTests
Scenario Outline: Calculator postive tests
	Given the test scenario is to '<Scenario>'
	And the left number is <LeftNumber>
	And the right number is <RightNumber>
	And the operator is <Operator>
	When Calculate button is clicked
	Then the calculation result should be <ExpectedResult>

Examples:
| Scenario                                                                 | LeftNumber | Operator | RightNumber | ExpectedResult |
| Verify the addition of 2 positive numbers                                | 999        | +        | 999         | 1998           |
| Verify the addition of 2 negative numbers                                | -99        | +        | -99         | -198           |
| Verify the addition of 1 positive number and 1 negative number           | 123        | +        | -23         | 100            |
| Verify the substraction of 2 positive numbers                            | 999        | -        | 999         | 0              |
| Verify the substraction of 2 positive numbers                            | -99        | -        | -99         | 0              |
| Verify the substraction of 2 positive numbers                            | 123        | -        | -77         | 200            |
| Verify the the multiplication of 2 positive numbers                      | 999        | *        | 999         | 998001         |
| Verify the the multiplication of 2 negative numbers                      | -99        | *        | -99         | 9801           |
| Verify the the multiplication of 1 positive number and 1 negative number | 99         | *        | -99         | -9801          |
| Verify the division of 2 positive numbers                                | 999        | /        | 999         | 1              |
| Verify the division of 2 negative numbers                                | -99        | /        | -99         | 1              |
| Verify the division of 1 positive number and 1 negative number           | 99         | /        | -99         | -1             |
| Verify the division of zero by any number                                | 0          | /        | 999         | 0              |

@NegativeTests
Scenario Outline: Calculator negative tests
	Given the left number is <LeftNumber>
	And the right number is <RightNumber>
	And the operator is <Operator>
	When Calculate button is clicked
	Then the calculation result should be <ExpectedResult>

Examples:
| Scenario                                | LeftNumber | Operator | RightNumber | ExpectedResult |
| Verify the division of a number by zero | 999        | /        | 0           | 0              |
