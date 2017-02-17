using System;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.Perceptrons;
using SodiumPlusUnitTests.Mocks;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class ErrorBackPropagationStepsTests
    {
        private NetworkBuilder _network;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
        }

        [Test]
        public void ErrorBackPropagationStepsThrowsExceptionIfLearningRateIsZero()
        {
            var inputs = _network.GetInputs();
            var weightSetterMock = new Mock<IWeightSetter>();
            var perceptronMock = new Mock<IPerceptronUnderTraining>();
            var exception = Assert.Throws<ArgumentException>(() => new ErrorBackPropagationStepsOnline(weightSetterMock.Object, perceptronMock.Object, 0d, 1d));
            exception.Message.Should().Be(ErrorBackPropagationStepsBase.LearningRateShouldNotBeZero);
        }
    }
}
