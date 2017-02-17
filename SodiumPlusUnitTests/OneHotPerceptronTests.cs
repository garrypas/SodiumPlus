using System.Collections.Generic;
using System.Linq;
using SodiumPlus.Perceptrons;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SodiumPlusUnitTests
{
    public class OneHotPerceptronTests
    {
        private IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>> _perceptron;
        private Mock<IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>> _wrappedPerceptronMock;

        [SetUp]
        public void SetUp()
        {
            _wrappedPerceptronMock = new Mock<IPerceptron<IUnit, IConnection, IUnitActivation<IUnit>>>();
            _perceptron = new OneHotPerceptron(_wrappedPerceptronMock.Object);
        }

        [Test]
        public async Task OneHotPerceptronReturnsAvgMaxAs1()
        {
            var expected = new List<double> { 0d, 1d, 0d };
            var results = new List<double> { 1d, 3d, 2d }.AsEnumerable();
            _wrappedPerceptronMock.Setup(p => p.FireAsync(It.IsAny<IEnumerable<double>>())).Returns(() => Task.Run(() => results));
            var oneHot = await _perceptron.FireAsync(new List<double>());
            oneHot.Should().BeEquivalentTo(expected);
        }
    }
}
