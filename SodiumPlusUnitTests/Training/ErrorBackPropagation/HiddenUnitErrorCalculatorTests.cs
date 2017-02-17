using SodiumPlusTraining.ErrorBackPropagation;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.Topology
{
    public class HiddenUnitErrorCalculatorTests
    {
        private HiddenUnitErrorCalculator _calculator;
        const double HiddenUnitDerivative = 0.6d;
        const double HiddenUnitActivation = 0.2d;
        private readonly static IEnumerable<double> PreviousLayerErrors = new List<double>{ 0.75d, 0.65d };
        private readonly static IEnumerable<double> WeightsToUpperUnits = new List<double>{ 0.5d, 0.6d };


        [SetUp]
        public void SetUp()
        {
            _calculator = new HiddenUnitErrorCalculator();
        }

        [Test]
        public void HiddenUnitsErrorsAreCalculatedCorrectly()
        {
            // δh1 = h'1 * ( w(h1o1) * δo1 + w(h1o2) * δo2 )
            const double expectedErrorOnHidden1 = (0.5d * 0.75d + 0.6d * 0.65d) * HiddenUnitDerivative;
            var actualError = _calculator.CalculateHiddenError(PreviousLayerErrors, WeightsToUpperUnits, HiddenUnitActivation, HiddenUnitDerivative);
            actualError.Should().BeApproximately(expectedErrorOnHidden1, 0.00000001d);
        }
    }
}
