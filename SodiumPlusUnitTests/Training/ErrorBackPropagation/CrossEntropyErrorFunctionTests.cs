using System;
using SodiumPlusTraining.ErrorBackPropagation.NetworkErrorFunctions;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class CrossEntropyErrorFunctionTests
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
        public void CrossEntropyErrorFunctionTestsCalculatesNetworkErrorCorrectly()
        {
            var expectedError = -(
                Math.Log(_actualValues[0]) * _idealValues[0]
                + Math.Log(_actualValues[1]) * _idealValues[1]
                + Math.Log(_actualValues[2])*_idealValues[2]
            );

            var errorFunction = new CrossEntropyErrorFunction();
            errorFunction.Calculate(_idealValues, _actualValues).Should().BeApproximately(expectedError, 0.00000001d);
        }
    }
}
