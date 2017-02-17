using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class SoftmaxUnitActivationTests
    {
        private SoftmaxUnitActivation<IUnit, IConnection, IUnitActivation<IUnit>> _softmaxUnitActivation;
        private const double NetInput = 0.1d;
        private IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>>> _network;
        private IdentityUnitActivation<IUnit> _otherUnitActivation;
        private Mock<ISoftmaxActivationFunction> _activationFunctionMock;

        [SetUp]
        public void SetUp()
        {
            _activationFunctionMock = new Mock<ISoftmaxActivationFunction>();
            _softmaxUnitActivation = new SoftmaxUnitActivation<IUnit, IConnection, IUnitActivation<IUnit>>(_activationFunctionMock.Object)
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
            _otherUnitActivation = new IdentityUnitActivation<IUnit>
            {
                Properties = new Unit()
            };

            var input1 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IdentityUnitActivation<IUnit>>();
            var input2 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IdentityUnitActivation<IUnit>>();
            var hidden1 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, SoftmaxUnitActivation<IUnit, IConnection, IUnitActivation<IUnit>>>(_softmaxUnitActivation);
            var hidden2 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IdentityUnitActivation<IUnit>>(_otherUnitActivation);
            var output1 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IdentityUnitActivation<IUnit>>();
            var output2 = TraversableUnit<IUnit, IConnection, IUnitActivation<IUnit>>.CreateUnit<Unit, IdentityUnitActivation<IUnit>>();

            _network = new List<ICollection<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>>>
            {
                new List<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> { input1, input2 },
                new List<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> { hidden1, hidden2 },
                new List<ITraversableUnitReadOnly<IUnit, IConnection, IUnitActivation<IUnit>>> { output1, output2 },
            };

            _softmaxUnitActivation.Network = _network;
        }

        [Test]
        public void SoftmaxUnitActivationIsNormalUnit()
        {
            _softmaxUnitActivation.UnitType.Should().Be(UnitType.NormalUnit);
        }

        [Test]
        public void SoftmaxUnitActivationFindsUnitLayerInNetwork()
        {
            _softmaxUnitActivation.LayerIndex.Should().Be(1);
        }

        [Test]
        public void SoftmaxUnitActivationPassesNetInputAndOtherNetInputsWhenCalculatingActivationValue()
        {
            _otherUnitActivation.Properties.NetInput = 0.88d;
            _softmaxUnitActivation.Properties.NetInput = 0.77d;
            _softmaxUnitActivation.Activate();

            _activationFunctionMock.Verify(f => f.Activation(_softmaxUnitActivation.NetInput, It.Is<IEnumerable<double>>(others => others.Count() == 2 && others.ElementAt(0) == _softmaxUnitActivation.NetInput && others.ElementAt(1) == _otherUnitActivation.NetInput)), Times.Once());
        }
    }
}
