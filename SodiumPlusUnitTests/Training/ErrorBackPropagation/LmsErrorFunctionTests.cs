using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class LmsErrorFunctionTests
    {
        private List<double> _idealValues;
        private List<double> _actualValues;

        [SetUp]
        public void SetUp()
        {
            _idealValues = new List<double> { 4d, 40d, 400d };
            _actualValues = new List<double> { 1d, 10d, 100d };
        }

        [Test]
        public void LmsErrorFunctionCalculatesNetworkErrorCorrectly()
        {
            const double expectedLms = 45454.5d;
            var errorFunction = new LmsErrorFunction();
            errorFunction.Calculate(_idealValues, _actualValues).Should().BeApproximately(expectedLms, 0.00000001d);
        }
    }
}
