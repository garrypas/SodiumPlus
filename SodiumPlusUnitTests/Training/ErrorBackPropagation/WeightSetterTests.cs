using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.Randomness;
using Moq;
using NUnit.Framework;
using System;
using SodiumPlusTraining.Topology;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class WeightSetterTests
    {
        private WeightSetter _weightSetter;
        private Mock<IRandomNumberGenerator> _randomNumberGeneratorMock;
        private Func<double> _getRandomNumber = () => 0d;
        private Mock<IConnectionUnderTraining> _connectionMock;

        [SetUp]
        public void SetUp()
        {
            _connectionMock = new Mock<IConnectionUnderTraining>();
            _randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            _randomNumberGeneratorMock.Setup(r => r.GenerateRandomNumber()).Returns(() => _getRandomNumber());
            _weightSetter = new WeightSetter(randomNumberGenerator: _randomNumberGeneratorMock.Object);
        }

        [Test]
        public void WeightInitializerGeneratesMinimumValue()
        {
            _getRandomNumber = () => 0d;
            _weightSetter.RangeFrom = -2;
            _weightSetter.RangeTo = 2;
            _weightSetter.SetWeight(_connectionMock.Object);
            _randomNumberGeneratorMock.Verify(r => r.GenerateRandomNumber(), Times.Once());
            _connectionMock.VerifySet(c => c.Weight = -2, Times.Once());
        }

        [Test]
        public void WeightInitializerGeneratesMaximumValue()
        {
            _getRandomNumber = () => 1d;
            _weightSetter.RangeFrom = -2;
            _weightSetter.RangeTo = 2;
            _weightSetter.SetWeight(_connectionMock.Object);
            _randomNumberGeneratorMock.Verify(r => r.GenerateRandomNumber(), Times.Once());
            _connectionMock.VerifySet(c => c.Weight = 2, Times.Once());
        }
    }
}
