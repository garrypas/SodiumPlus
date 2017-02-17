using SodiumPlusTraining.ErrorBackPropagation;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class OutputUnitErrorCalculatorTests
    {
        private OutputUnitErrorCalculator _calculator;
        const double OutputActivation = 0.5d;
        const double OutputDerivative = 2d;
        const double IdealValue = 0.9d;

        [SetUp]
        public void SetUp()
        {
            _calculator = new OutputUnitErrorCalculator();
        }

        [Test]
        public void OutputUnitsErrorsAreCalculatedCorrectly()
        {
            // δo1 = o'1 * ( d1 - o1 )
            const double expectedErrorOnHidden1 = OutputDerivative * (IdealValue - 0.5d);
            var actualError = _calculator.CalculateOutputError(IdealValue, OutputActivation, OutputDerivative);
            actualError.Should().BeApproximately(expectedErrorOnHidden1, 0.00000001d);
        }
    }
}
