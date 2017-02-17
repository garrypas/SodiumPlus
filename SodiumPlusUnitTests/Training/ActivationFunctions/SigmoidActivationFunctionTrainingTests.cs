using SodiumPlus.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;
using SodiumPlusTraining.ActivationFunctions;

namespace SodiumPlusUnitTests.Training.ActivationFunctions
{
    public class SigmoidActivationFunctionTrainingTests
    {
        private SigmoidActivationFunctionTraining _activationFunction;

        [SetUp]
        public void SetUp()
        {
            _activationFunction = new SigmoidActivationFunctionTraining();
        }

        [Test]
        public void SigmoidActivationFunctionTrainingReturnsExpectedActivation()
        {
            var expected = new SigmoidActivationFunction().Activation(0.6d);
            _activationFunction.Activation(0.6).Should().BeApproximately(expected, 0.00000001d);
        }

        [Test]
        public void SigmoidActivationFunctionTrainingReturnsExpectedDerivative()
        {
            const double expected = 0.22878424d;
            var activation = _activationFunction.Activation(0.6d);
            _activationFunction.Derivative(activation).Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
