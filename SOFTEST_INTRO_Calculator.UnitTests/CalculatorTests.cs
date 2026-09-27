using SOFTEST_INTRO_Calculator;
using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

// This class holds automated tests for the Calculator class.
// NUnit is the testing framework: it finds every method marked [Test] or
// [TestCase] in this class and runs each one independently, reporting
// pass/fail for each.
public class CalculatorTests
{
    // Shared Calculator instance used by every test method below.
    // "null!" tells the compiler "trust me, this will be set before use" —
    // it gets assigned fresh in SetUp() before each test runs.
    private Calculator _calculator = null!;

    // [SetUp] marks a method that NUnit runs automatically before EVERY
    // single test in this class. This guarantees each test starts with a
    // brand-new Calculator, so tests can't accidentally affect each other.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // [Test] marks a single, standalone test case with no parameters.
    // Most tests follow the "Arrange, Act, Assert" pattern:
    //   Arrange = set up the inputs/objects needed
    //   Act     = call the method being tested
    //   Assert  = check the result is what we expected
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange: the calculator is created in Setup
        // Act
        double result = _calculator.Add(10, 20);

        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    // [TestCase] lets one test method run multiple times with different
    // input values, instead of copy-pasting the same test over and over.
    // Each line below is one run: (a, b, expected).
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(
        double a, double b, double expected)
    {
        // Act
        double result = _calculator.Add(a, b);

        // Assert
        // "Within(1e-9)" allows a tiny rounding error, since decimal values
        // like 0.1 + 0.2 can't be represented exactly in binary floating point.
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 0, 0)]
    [TestCase(5, 0, 5)]
    [TestCase(3, 10, -7)]
    [TestCase(-4, -9, 5)]
    public void Subtract_RepresentativeInputs_ReturnsDifference(
        double a, double b, double expected)
    {
        // Act
        double result = _calculator.Subtract(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(6, 7, 42)]
    [TestCase(9, 0, 0)]
    [TestCase(-4, 3, -12)]
    [TestCase(-4, -3, 12)]
    public void Multiply_RepresentativeInputs_ReturnsProduct(
        double a, double b, double expected)
    {
        // Act
        double result = _calculator.Multiply(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(1, 2, 0.5)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_ValidInputs_ReturnsQuotient(
        double a, double b, double expected)
    {
        // Act
        double result = _calculator.Divide(a, b);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Dividing by zero should throw an exception rather than return a value.
    // "Throws.TypeOf<...>()" checks that calling the code inside the lambda
    // (() => ...) raises that specific exception type.
    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
                    Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);
        Assert.That(result, Is.EqualTo(1L));
    }

    [TestCase(0, 1L)]
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsFactorial(int n, long expected)
    {
        long result = _calculator.Factorial(n);
        Assert.That(result, Is.EqualTo(expected));
    }

    // Factorial only accepts 0-20 (see Calculator.cs), so these inputs
    // should throw instead of returning a number.
    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_OutOfRange_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void TriangleArea_PositiveDimensions_ReturnsHalfBaseTimesHeight()
    {
        double result = _calculator.TriangleArea(3, 4);
        Assert.That(result, Is.EqualTo(6).Within(1e-9));
    }

    [TestCase(0, 4, 0)]
    [TestCase(3, 0, 0)]
    [TestCase(0, 0, 0)]
    public void TriangleArea_ZeroDimension_ReturnsZero(
        double height, double width, double expected)
    {
        double result = _calculator.TriangleArea(height, width);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-3, 4)]
    [TestCase(3, -4)]
    [TestCase(-3, -4)]
    public void TriangleArea_NegativeDimension_ThrowsArgumentOutOfRangeException(
        double height, double width)
    {
        Assert.That(() => _calculator.TriangleArea(height, width),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void CircleArea_RadiusOne_ReturnsPi()
    {
        double result = _calculator.CircleArea(1);
        Assert.That(result, Is.EqualTo(Math.PI).Within(1e-9));
    }

    [Test]
    public void CircleArea_RadiusZero_ReturnsZero()
    {
        double result = _calculator.CircleArea(0);
        Assert.That(result, Is.EqualTo(0).Within(1e-9));
    }

    [TestCase(2, 4 * Math.PI)]
    [TestCase(0.5, 0.25 * Math.PI)]
    public void CircleArea_PositiveRadius_ReturnsPiRSquared(
        double radius, double expected)
    {
        double result = _calculator.CircleArea(radius);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1)]
    [TestCase(-0.5)]
    public void CircleArea_NegativeRadius_ThrowsArgumentOutOfRangeException(double radius)
    {
        Assert.That(() => _calculator.CircleArea(radius),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Section 7 extension: A is permutations P(n, r) = n! / (n - r)!
    [TestCase(5, 5, 120L)]
    [TestCase(5, 4, 120L)]
    [TestCase(5, 3, 60L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 30L)]   // extra discriminating case: A=30 here, B=15
    public void UnknownFunctionA_ValidInputs_ReturnsPermutations(int n, int r, long expected)
    {
        Assert.That(_calculator.UnknownFunctionA(n, r), Is.EqualTo(expected));
    }

    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 0)]
    [TestCase(5, -1)]
    public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionA(n, r),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Section 7 extension: B is combinations C(n, r) = n! / (r! * (n - r)!)
    [TestCase(5, 5, 1L)]
    [TestCase(5, 4, 5L)]
    [TestCase(5, 3, 10L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 15L)]   // extra discriminating case: B=15 here, A=30
    public void UnknownFunctionB_ValidInputs_ReturnsCombinations(int n, int r, long expected)
    {
        Assert.That(_calculator.UnknownFunctionB(n, r), Is.EqualTo(expected));
    }

    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 0)]
    [TestCase(5, -1)]
    public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionB(n, r),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Lab 2 Part II: MTBF = operating time / number of failures.
    [TestCase(200, 4, 50)]
    [TestCase(90, 9, 10)]
    [TestCase(1, 1, 1)]
    public void MeanTimeBetweenFailures_ValidInputs_ReturnsMtbf(
        double operatingTime, double failureCount, double expected)
    {
        double result = _calculator.MeanTimeBetweenFailures(operatingTime, failureCount);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 4)]
    [TestCase(-200, 4)]
    [TestCase(200, 0)]
    [TestCase(200, -4)]
    public void MeanTimeBetweenFailures_NonPositiveInputs_ThrowsArgumentOutOfRangeException(
        double operatingTime, double failureCount)
    {
        Assert.That(() => _calculator.MeanTimeBetweenFailures(operatingTime, failureCount),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Lab 2 Part II: Availability = MTBF / (MTBF + MTTR).
    [TestCase(150, 50, 0.75)]
    [TestCase(90, 10, 0.9)]
    [TestCase(0, 10, 0)]
    public void Availability_ValidInputs_ReturnsRatio(
        double mtbf, double mttr, double expected)
    {
        double result = _calculator.Availability(mtbf, mttr);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1, 10)]
    [TestCase(10, -1)]
    public void Availability_NegativeInputs_ThrowsArgumentOutOfRangeException(
        double mtbf, double mttr)
    {
        Assert.That(() => _calculator.Availability(mtbf, mttr),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Availability_BothZero_ThrowsArgumentException()
    {
        Assert.That(() => _calculator.Availability(0, 0),
                    Throws.TypeOf<ArgumentException>());
    }

    // Lab 2 Part II: Basic Musa model. lambda(tau) = lambda0 * exp(-lambda0*tau/nu0).
    [Test]
    public void BasicMusaFailureIntensity_TauZero_ReturnsLambda0()
    {
        double result = _calculator.BasicMusaFailureIntensity(10, 100, 0);
        Assert.That(result, Is.EqualTo(10).Within(1e-9));
    }

    [Test]
    public void BasicMusaFailureIntensity_NormalTau_ReturnsExpected()
    {
        double result = _calculator.BasicMusaFailureIntensity(10, 100, 10);
        Assert.That(result, Is.EqualTo(3.678794411714420).Within(1e-9));
    }

    [TestCase(0, 100, 10)]
    [TestCase(-5, 100, 10)]
    [TestCase(10, 0, 10)]
    [TestCase(10, -100, 10)]
    [TestCase(10, 100, -1)]
    public void BasicMusaFailureIntensity_InvalidInputs_ThrowsArgumentOutOfRangeException(
        double lambda0, double nu0, double tau)
    {
        Assert.That(() => _calculator.BasicMusaFailureIntensity(lambda0, nu0, tau),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Lab 2 Part II: Basic Musa model. mu(tau) = nu0 * [1 - exp(-lambda0*tau/nu0)].
    [Test]
    public void BasicMusaCumulativeFailures_TauZero_ReturnsZero()
    {
        double result = _calculator.BasicMusaCumulativeFailures(10, 100, 0);
        Assert.That(result, Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void BasicMusaCumulativeFailures_NormalTau_ReturnsExpected()
    {
        double result = _calculator.BasicMusaCumulativeFailures(10, 100, 10);
        Assert.That(result, Is.EqualTo(63.212055882855800).Within(1e-9));
    }

    [TestCase(0, 100, 10)]
    [TestCase(-5, 100, 10)]
    [TestCase(10, 0, 10)]
    [TestCase(10, -100, 10)]
    [TestCase(10, 100, -1)]
    public void BasicMusaCumulativeFailures_InvalidInputs_ThrowsArgumentOutOfRangeException(
        double lambda0, double nu0, double tau)
    {
        Assert.That(() => _calculator.BasicMusaCumulativeFailures(lambda0, nu0, tau),
                    Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}
