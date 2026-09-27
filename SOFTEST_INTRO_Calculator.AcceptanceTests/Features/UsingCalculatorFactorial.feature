@Factorial
Feature: UsingCalculatorFactorial
  In order to count arrangements accurately
  As a calculator user
  I want to calculate the factorial of a number

  Scenario: Calculate a normal factorial
    Given I have a calculator
    When I have entered 5 into the calculator and press factorial
    Then the factorial result should be 120

  Scenario: Factorial of zero is the identity case
    Given I have a calculator
    When I have entered 0 into the calculator and press factorial
    Then the factorial result should be 1

  Scenario: Reject an unsupported factorial value
    Given I have a calculator
    When I have entered -1 into the calculator and press factorial
    Then the factorial should be rejected
