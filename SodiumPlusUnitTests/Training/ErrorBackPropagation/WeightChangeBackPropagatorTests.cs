using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusUnitTests.Mocks;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class WeightChangeBackPropagatorTests
    {
        private WeightChangeBackPropagator _weightChangeBackPropagator;

        private NetworkBuilder _network;
        private Mock<IWeightChangeApplier> _weightChangeApplierMock;
        private WeightChangeCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();
            _weightChangeApplierMock = new Mock<IWeightChangeApplier>();
            _calculator = new WeightChangeCalculator(0.1);
            _weightChangeBackPropagator = new WeightChangeBackPropagator(_weightChangeApplierMock.Object, _network.GetNetwork(), learningRate: 0.1);
        }

        [Test]
        public async Task WeightChangeBackPropagatorUpdatesWeights()
        {
            var expectedChangeInput1ToHidden1 = _calculator.CalculateChange(_network.Input1Activation, _network.Hidden1Error);
            var expectedChangeInput1ToHidden2 = _calculator.CalculateChange(_network.Input1Activation, _network.Hidden2Error);
            var expectedChangeInput2ToHidden1 = _calculator.CalculateChange(_network.Input2Activation, _network.Hidden1Error);
            var expectedChangeInput2ToHidden2 = _calculator.CalculateChange(_network.Input2Activation, _network.Hidden2Error);

            var expectedChangeHidden1ToOutput1 = _calculator.CalculateChange(_network.Hidden1Activation, _network.Output1Error);
            var expectedChangeHidden1ToOutput2 = _calculator.CalculateChange(_network.Hidden1Activation, _network.Output2Error);
            var expectedChangeHidden2ToOutput1 = _calculator.CalculateChange(_network.Hidden2Activation, _network.Output1Error);
            var expectedChangeHidden2ToOutput2 = _calculator.CalculateChange(_network.Hidden2Activation, _network.Output2Error);

            await _weightChangeBackPropagator.UpdateAllWeightsAsync();
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionInput1Hidden1.Properties, expectedChangeInput1ToHidden1), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionInput1Hidden2.Properties, expectedChangeInput1ToHidden2), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionInput2Hidden1.Properties, expectedChangeInput2ToHidden1), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionInput2Hidden2.Properties, expectedChangeInput2ToHidden2), Times.Once());

            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionHidden1Output1.Properties, expectedChangeHidden1ToOutput1), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionHidden1Output2.Properties, expectedChangeHidden1ToOutput2), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionHidden2Output1.Properties, expectedChangeHidden2ToOutput1), Times.Once());
            _weightChangeApplierMock.Verify(wca => wca.ApplyWeightChange(_network.ConnectionHidden2Output2.Properties, expectedChangeHidden2ToOutput2), Times.Once());
        }
    }
}
