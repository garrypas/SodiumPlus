using System.Collections.Generic;
using SodiumPlusUnitTests.Mocks;
using FluentAssertions;
using NUnit.Framework;

namespace SodiumPlusUnitTests.Training.Extensions
{
    public class UnitUnderTrainingEnumerableExtensionsTests
    {
        private NetworkBuilder _network;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
        }

        [Test]
        public void UnitUnderTrainingExtensionsGetsDescendents()
        {
            var descendents = _network.Output1.GetAllInputsRecursive();
            descendents.Should().Contain(new List<object> {
                _network.Input1,
                _network.Input2,
                _network.Hidden1,
                _network.Hidden2
            }).And.HaveCount(4);
        }
    }
}
