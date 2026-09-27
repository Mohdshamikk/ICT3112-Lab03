using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorBasicReliabilitySteps
{
    private readonly CalculatorContext _context;

    public UsingCalculatorBasicReliabilitySteps(CalculatorContext context)
        => _context = context;

    [When("I have entered {double}, {double} and {double} into the calculator and press failure intensity")]
    public void WhenIHaveEnteredAndPressFailureIntensity(double lambda0, double nu0, double tau)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.BasicMusaFailureIntensity(lambda0, nu0, tau);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }

    [When("I have entered {double}, {double} and {double} into the calculator and press cumulative failures")]
    public void WhenIHaveEnteredAndPressCumulativeFailures(double lambda0, double nu0, double tau)
    {
        _context.Result = null;
        _context.Error = null;

        try
        {
            _context.Result = _context.Calculator.BasicMusaCumulativeFailures(lambda0, nu0, tau);
        }
        catch (ArgumentOutOfRangeException error)
        {
            _context.Error = error;
        }
    }
}
