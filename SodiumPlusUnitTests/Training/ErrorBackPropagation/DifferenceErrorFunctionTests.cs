using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class DifferenceErrorFunctionTests
    {
        [Test]
        public void DifferenceErrorFunctionCalculatesNetworkErrorCorrectly()
        {
            const double expectedDifference = 333d;
            var idealValues = new List<double> { 4d, 40d, 400d };
            var actualValues = new List<double> { 1d, 10d, 100d };
            var errorFunction = new DifferenceErrorFunction();
            errorFunction.Calculate(idealValues, actualValues).Should().BeApproximately(expectedDifference, 0.00000001d);
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void DifferenceErrorFunctionReturnsAnAbsoluteValue(double desired, double actual)
        {
            const double expectedDifference = 1d;
            var errorFunction = new DifferenceErrorFunction();
            errorFunction.Calculate(new [] { desired }, new [] { actual }).Should().BeApproximately(expectedDifference, 0.00000001d);
        }
    }
}
