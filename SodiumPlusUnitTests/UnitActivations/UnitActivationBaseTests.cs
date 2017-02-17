using SodiumPlus.ActivationFunctions;
using SodiumPlus.Topology;
using SodiumPlus.UnitActivations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace SodiumPlusUnitTests.UnitActivations
{
    public class UnitActivationBaseTests
    {
        private UnitActivationBase<Unit> _unitActivationBase;
        private Mock<IActivationFunction> _activationFunctionMock;
        private const double NetInput = 0.1d;

        [SetUp]
        public void SetUp()
        {
            _activationFunctionMock = new Mock<IActivationFunction>();
            _activationFunctionMock.Setup(af => af.Activation(It.IsAny<double>())).Returns<double>(x => x + 1d);
            _unitActivationBase = new UnitActivationBaseImpl<Unit>(_activationFunctionMock.Object, UnitType.NormalUnit)
            {
                Properties = new Unit
                {
                    NetInput = NetInput
                }
            };
        }

        [Test]
        public void UnitActivationBaseStimulates()
        {
            const double netInput = 0.2d;
            _unitActivationBase.Stimulate(netInput);
            _unitActivationBase.Properties.NetInput.Should().Be(netInput);
        }

        [Test]
        public void UnitActivationBaseActivatesUsingActivationFunction()
        {
            _unitActivationBase.Stimulate(NetInput);
            _unitActivationBase.Activate();
            _activationFunctionMock.Verify(af => af.Activation(NetInput), Times.Once());
        }

        [Test]
        public void UnitActivationBaseActivateSetsActivationValue()
        {
            _unitActivationBase.Stimulate(NetInput);
            _unitActivationBase.Activate();
            _unitActivationBase.Properties.ActivationValue.Should().BeApproximately(NetInput + 1d, 0.00000001d);
        }

        private class UnitActivationBaseImpl<TUnit> : UnitActivationBase<TUnit> where TUnit : IUnit {
            private readonly UnitType _unitType;

            public UnitActivationBaseImpl(IActivationFunction activationFunction, UnitType unitType) : base(activationFunction)
            {
                _unitType = unitType;
            }

            public override UnitType UnitType { get { return _unitType; } }
        }
        
    }
}
