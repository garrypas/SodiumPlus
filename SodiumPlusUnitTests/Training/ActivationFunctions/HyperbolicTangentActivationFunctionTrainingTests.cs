using System;
using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;
using SodiumPlusTraining.ActivationFunctions;

namespace SodiumPlusUnitTests.Training.ActivationFunctions
{
    public class HyperbolicTangentActivationFunctionTrainingTests
    {
        private HyperbolicTangentActivationFunctionTraining _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new HyperbolicTangentActivationFunctionTraining();
        }

        [Test]
        public void HyperbolicActivationFunctionTrainingReturnsExpectedDerivative()
        {
            const double expected = 0.7115777626;
            _activationFunction.Derivative(0.6).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}