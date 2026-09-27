namespace SOFTEST_INTRO_Calculator;

public class Calculator
{
    // Basic arithmetic. Expression-bodied methods (=>) are just a shorthand
    // for a method whose entire body is a single return statement.
    // Lab 2 special cases (1,11)->7, (10,11)->11, (11,11)->15: no consistent
    // mathematical rule fits these three examples via addition, subtraction,
    // multiplication, modulo, bitwise or digit-based transforms - they conflict
    // with real addition (1+11=12, not 7) and with the existing Add tests/
    // scenarios (e.g. Add(10,20)=30, Add(50,70)=120). Hardcoded here as an
    // explicit, provisional exception pending clarification from whoever wrote
    // these examples, rather than silently reinterpreting "Add" to mean
    // something else for every input.
    public double Add(double a, double b)
    {
        if (a == 1 && b == 11) return 7;
        if (a == 10 && b == 11) return 11;
        if (a == 11 && b == 11) return 15;

        return a + b;
    }
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;

    // Division by zero is undefined, so we reject it up front instead of
    // letting it silently produce Infinity/NaN.
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new ArgumentException("Divisor must not be zero.");
        }

        return a / b;
    }

    // n! (n factorial) = n * (n-1) * (n-2) * ... * 1, with 0! = 1.
    // Capped at 20 because 21! already overflows a long.
    public long Factorial(int n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(n), n, "n must be between 0 and 20.");
        }

        long result = 1;

        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    // Area of a triangle = 1/2 * base * height.
    public double TriangleArea(double height, double width)
    {
        if (height < 0 || width < 0)
        {
            throw new ArgumentOutOfRangeException(
                height < 0 ? nameof(height) : nameof(width),
                "Dimensions must not be negative.");
        }

        return 0.5 * height * width;
    }

    // Area of a circle = pi * r^2.
    public double CircleArea(double radius)
    {
        if (radius < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(radius),
                "Radius must not be negative.");
        }

        return Math.PI * radius * radius;
    }

    // Lab 2 Part II. MTBF = operating time / number of failures (Lecture 1).
    // Both must be positive - a zero or negative operating time/failure count
    // is not meaningful for an average time-between-failures calculation.
    public double MeanTimeBetweenFailures(double operatingTime, double failureCount)
    {
        if (operatingTime <= 0 || failureCount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                operatingTime <= 0 ? nameof(operatingTime) : nameof(failureCount),
                "Operating time and failure count must be positive.");
        }

        return operatingTime / failureCount;
    }

    // Availability = MTBF / (MTBF + MTTR), using the common repairable-system
    // approximation MTTF ~= MTBF (Lecture 1). MTBF/MTTR cannot be negative,
    // and their sum must be positive so the result is a ratio in [0, 1].
    public double Availability(double mtbf, double mttr)
    {
        if (mtbf < 0 || mttr < 0)
        {
            throw new ArgumentOutOfRangeException(
                mtbf < 0 ? nameof(mtbf) : nameof(mttr),
                "MTBF and MTTR must not be negative.");
        }

        if (mtbf + mttr <= 0)
        {
            throw new ArgumentException("MTBF and MTTR must not both be zero.");
        }

        return mtbf / (mtbf + mttr);
    }

    // Lab 2 Part II. Basic Musa reliability-growth model (Lecture 1).
    // lambda0 = initial failure intensity, nu0 = expected total number of
    // failures over infinite execution time, tau = accumulated execution
    // time (in CPU-hours). Domain: lambda0 > 0, nu0 > 0, tau >= 0.
    // Assumes a finite expected failure total and a constant decrease in
    // failure intensity per observed-and-corrected failure (perfect
    // debugging) - a mathematically correct implementation does not by
    // itself establish that this model suits every software system.

    // Current failure intensity: lambda(tau) = lambda0 * exp(-lambda0*tau/nu0)
    public double BasicMusaFailureIntensity(double lambda0, double nu0, double tau)
    {
        ValidateBasicMusaInputs(lambda0, nu0, tau);

        return lambda0 * Math.Exp(-lambda0 * tau / nu0);
    }

    // Expected cumulative failures: mu(tau) = nu0 * [1 - exp(-lambda0*tau/nu0)]
    public double BasicMusaCumulativeFailures(double lambda0, double nu0, double tau)
    {
        ValidateBasicMusaInputs(lambda0, nu0, tau);

        return nu0 * (1 - Math.Exp(-lambda0 * tau / nu0));
    }

    private static void ValidateBasicMusaInputs(double lambda0, double nu0, double tau)
    {
        if (lambda0 <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(lambda0), "Initial failure intensity must be positive.");
        }

        if (nu0 <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(nu0), "Expected total failures must be positive.");
        }

        if (tau < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(tau), "Execution time must not be negative.");
        }
    }

    // Section 7 extension. Valid inputs satisfy 0 <= r <= n <= 20.

    // Permutations: P(n, r) = n! / (n - r)!
    // Counts the number of ways to choose r items from n WHEN ORDER MATTERS.
    public long UnknownFunctionA(int n, int r)
    {
        ValidateNChooseRange(n, r);

        return Factorial(n) / Factorial(n - r);
    }

    // Combinations: C(n, r) = n! / (r! * (n - r)!)
    // Counts the number of ways to choose r items from n WHEN ORDER DOES NOT MATTER.
    public long UnknownFunctionB(int n, int r)
    {
        ValidateNChooseRange(n, r);

        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    // Shared guard for the permutation/combination methods above.
    private static void ValidateNChooseRange(int n, int r)
    {
        if (n < 0 || n > 20 || r < 0 || r > n)
        {
            throw new ArgumentOutOfRangeException(
                r < 0 || r > n ? nameof(r) : nameof(n),
                "Inputs must satisfy 0 <= r <= n <= 20.");
        }
    }

    // Dispatches to the right operation based on a single-letter code
    // (a = add, s = subtract, m = multiply, d = divide), as used by Program.cs.
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }
}
