using SodiumPlusTraining.ActivationFunctions;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ActivationFunctions
{
    public class SoftmaxActivationFunctionTrainingTests
    {
        private SoftmaxActivationFunctionTraining _softmaxActivationFunctionTraining;

        [SetUp]
        public void SetUp()
        {
            _softmaxActivationFunctionTraining = new SoftmaxActivationFunctionTraining();
        }

        [Test]
        public void SoftmaxActivationFunctionCalculatesDerivativeCorrectly()
        {
            const double activation = 0.88d;
            var derivative = _softmaxActivationFunctionTraining.Derivative(activation);
            const double expected = activation * (1 - activation);
            derivative.Should().BeApproximately(expected, 0.00000001d);
        }
    }
}
