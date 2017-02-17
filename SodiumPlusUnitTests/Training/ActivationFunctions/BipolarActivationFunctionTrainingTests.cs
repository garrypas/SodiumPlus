using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;
using SodiumPlusTraining.ActivationFunctions;

namespace SodiumPlusUnitTests.Training.ActivationFunctions
{
    public class BipolarActivationFunctionTrainingTests
    {
        private BipolarActivationFunctionTraining _activationFunction;

        [SetUp]
        public void Setup()
        {
            _activationFunction = new BipolarActivationFunctionTraining();
        }

        [Test]
        public void BipolarActivationFunctionTrainingReturnsExpectedActivation()
        {
            var expected = new BipolarActivationFunction().Activation(0.6d);
            _activationFunction.Activation(0.6).Should().BeApproximately(expected, 0.00000001d);
        }

        [Test]
        public void BipolarActivationFunctionTrainingReturnsExpectedDerivative()
        {
            const double expected = 0.45756848d;
            var activation = _activationFunction.Activation(0.6d);
            _activationFunction.Derivative(activation).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}