using FluentAssertions;
using NUnit.Framework;
using SodiumPlusTraining.ActivationFunctions;

namespace SodiumPlusUnitTests.Training.ActivationFunctions
{
    public class ReluActivationFunctionTrainingTests
    {
        private ReluActivationFunctionTraining _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new ReluActivationFunctionTraining();
        }

        [Test]
        public void ReluActivationFunctionTrainingReturnsExpectedDerivative()
        {
            const double expected = 1d;
            var activation = _activationFunction.Activation(0.6d);
            _activationFunction.Derivative(activation).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}