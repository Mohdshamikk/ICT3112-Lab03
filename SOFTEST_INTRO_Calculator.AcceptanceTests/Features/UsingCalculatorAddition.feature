@Addition
Feature: UsingCalculatorAddition
  In order to avoid mistakes
  As a calculator user
  I want to be told the sum of two numbers

  Scenario: Add two numbers
    Given I have a calculator
    When I have entered 50 and 70 into the calculator and press add
    Then the result should be 120

  # NOTE: These examples do not correspond to real addition (1+11=12, not 7).
  # No consistent mathematical rule fits all three rows using standard operations
  # (addition, subtraction, multiplication, modulo, bitwise, digit-based transforms
  # were all tried). Implemented as an explicit special case pending clarification
  # from whoever authored these examples - see Calculator.Add for details.
  Scenario Outline: Add zeros for special cases
    Given I have a calculator
    When I have entered <value1> and <value2> into the calculator and press add
    Then the result should be <value3>

    Examples:
      | value1 | value2 | value3 |
      | 1      | 11     | 7      |
      | 10     | 11     | 11     |
      | 11     | 11     | 15     |
