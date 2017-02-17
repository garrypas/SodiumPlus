using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SodiumPlusTraining.ErrorBackPropagation;
using SodiumPlusTraining.Topology;
using SodiumPlusTraining.UnitActivations;
using SodiumPlusUnitTests.Mocks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SodiumPlus.Topology;

namespace SodiumPlusUnitTests.Training.ErrorBackPropagation
{
    public class ErrorValueBackPropagatorTests
    {
        private ErrorValueBackPropagator _backPropagator;
        private IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> _hiddenUnits;
        private IEnumerable<ITraversableUnitReadOnly<IUnitUnderTraining, IConnectionUnderTraining, IUnitActivationTraining>> _outputUnits;
        const double IdealValue1 = 0.9d;
        const double IdealValue2 = 0.8d;
        const double Output1ExpectedError = 77d;
        const double Output2ExpectedError = 9999d;
        const double Hidden1ExpectedError = 77d;
        const double Hidden2ExpectedError = 9999d;
        private Mock<IHiddenUnitErrorCalculator> _hiddenErrorCalculatorMock;
        private Mock<IOutputUnitErrorCalculator> _outputErrorCalculatorMock;
        private NetworkBuilder _network;
        private List<double> _idealValues;

        [SetUp]
        public void SetUp()
        {
            _network = new NetworkBuilder().Setup();

            _outputErrorCalculatorMock = new Mock<IOutputUnitErrorCalculator>();
            _outputErrorCalculatorMock.Setup(c => c.CalculateOutputError(IdealValue1, _network.Output1Activation, _network.Output1Derivative)).Returns(Output1ExpectedError);
            _outputErrorCalculatorMock.Setup(c => c.CalculateOutputError(IdealValue2, _network.Output2Activation, _network.Output2Derivative)).Returns(Output2ExpectedError);

            _hiddenErrorCalculatorMock = new Mock<IHiddenUnitErrorCalculator>();
            _hiddenErrorCalculatorMock.Setup(c => c.CalculateHiddenError(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>(), _network.Hidden1Activation, _network.Hidden1Derivative)).Returns(Hidden1ExpectedError);
            _hiddenErrorCalculatorMock.Setup(c => c.CalculateHiddenError(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>(), _network.Hidden2Activation, _network.Hidden2Derivative)).Returns(Hidden2ExpectedError);

            _backPropagator = new ErrorValueBackPropagator(_network.GetNetwork(), _outputErrorCalculatorMock.Object, _hiddenErrorCalculatorMock.Object);

            _outputUnits = _network.GetOutputs();
            _hiddenUnits = _network.GetHidden();

            _idealValues = new List<double> { IdealValue1, IdealValue2 };
        }

        [Test]
        public async Task ErrorValueBackPropagatorUpdatesAllOutputErrors()
        {
            await _backPropagator.BackPropagateAllErrorsAsync(_outputUnits, _idealValues);

            _outputErrorCalculatorMock.Verify(c => c.CalculateOutputError(IdealValue1, _network.Output1Activation, _network.Output1Derivative), Times.Once());
            _outputErrorCalculatorMock.Verify(c => c.CalculateOutputError(IdealValue2, _network.Output2Activation, _network.Output2Derivative), Times.Once());

            _outputUnits.ElementAt(0).UnitActivation.Error.Should().BeApproximately(Output1ExpectedError, 0.00000001d);
            _outputUnits.ElementAt(1).UnitActivation.Error.Should().BeApproximately(Output2ExpectedError, 0.00000001d);
        }

        [Test]
        public async Task ErrorValueBackPropagatorUpdatesAllHiddenErrors()
        {
            await _backPropagator.BackPropagateHiddenLayer(_hiddenUnits);

            _hiddenErrorCalculatorMock.Verify(c => c.CalculateHiddenError(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>(), _network.Hidden1Activation, _network.Hidden1Derivative), Times.Once());
            _hiddenErrorCalculatorMock.Verify(c => c.CalculateHiddenError(It.IsAny<IEnumerable<double>>(), It.IsAny<IEnumerable<double>>(), _network.Hidden2Activation, _network.Hidden2Derivative), Times.Once());

            _hiddenUnits.ElementAt(0).UnitActivation.Error.Should().BeApproximately(Hidden1ExpectedError, 0.00000001d);
            _hiddenUnits.ElementAt(1).UnitActivation.Error.Should().BeApproximately(Hidden2ExpectedError, 0.00000001d);
        }
    }
}
