@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate the Basic Musa model's failures and intensities
  As a Software Quality Metric enthusiast
  I want to use my calculator to do this

  Scenario: Current failure intensity at the start of execution
    Given I have a calculator
    When I have entered 10, 100 and 0 into the calculator and press failure intensity
    Then the result should be 10

  Scenario: Current failure intensity after some execution time
    Given I have a calculator
    When I have entered 10, 100 and 10 into the calculator and press failure intensity
    Then the result should be 3.678794411714420

  Scenario: Expected cumulative failures at the start of execution
    Given I have a calculator
    When I have entered 10, 100 and 0 into the calculator and press cumulative failures
    Then the result should be 0

  Scenario: Expected cumulative failures after some execution time
    Given I have a calculator
    When I have entered 10, 100 and 10 into the calculator and press cumulative failures
    Then the result should be 63.212055882855800

  Scenario: Rejecting a negative execution time
    Given I have a calculator
    When I have entered 10, 100 and -1 into the calculator and press failure intensity
    Then the calculation should be rejected
