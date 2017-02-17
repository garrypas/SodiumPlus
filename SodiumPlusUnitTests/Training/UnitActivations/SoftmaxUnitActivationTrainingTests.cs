using SodiumPlus.Topology;
using SodiumPlusTraining.ActivationFunctions;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace SodiumPlusUnitTests.Training.UnitActivations
{
    public class SoftmaxUnitActivationTrainingTests
    {
        private Mock<ISoftmaxActivationFunctionTraining> _activationFunctionMock;
        private SoftmaxUnitActivationTraining _softmaxUnitActivationTraining;

        private IEnumerable<IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>> _network;
        private IUnitActivationTraining _otherUnitActivation;

        [SetUp]
        public void SetUp()
        {
            _activationFunctionMock = new Mock<ISoftmaxActivationFunctionTraining>();
            _softmaxUnitActivationTraining = new SoftmaxUnitActivationTraining(_activationFunctionMock.Object)
            {
                Properties = new UnitUnderTraining()
            };

            _otherUnitActivation = new IdentityUnitActivationTraining
            {
                Properties = new UnitUnderTraining()
            };
            

            var input1 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, IdentityUnitActivationTraining>();
            var input2 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, IdentityUnitActivationTraining>();
            var hidden1 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, IUnitActivationTraining>(_otherUnitActivation);
            var hidden2 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, SoftmaxUnitActivationTraining>(_softmaxUnitActivationTraining);
            var output1 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, IdentityUnitActivationTraining>();
            var output2 = TraversableUnit<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>.CreateUnit<UnitUnderTraining, IdentityUnitActivationTraining>();

            _network = new List<ICollection<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>>>
            {
                new List<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> { input1, input2 },
                new List<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> { hidden1, hidden2 },
                new List<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> { output1, output2 },
            };

            _softmaxUnitActivationTraining.Network = _network;
        }

        [Test]
        public void SoftmaxUnitActivationTrainingPassesActivationValueWhenCalculatingDerivative()
        {
            _otherUnitActivation.Properties.ActivationValue = 0.88d;
            _softmaxUnitActivationTraining.Properties.ActivationValue = 0.77d;
            _softmaxUnitActivationTraining.Derivative();

            _activationFunctionMock.Verify(f => f.Derivative(_softmaxUnitActivationTraining.Properties.ActivationValue), Times.Once());
        }
    }
}
